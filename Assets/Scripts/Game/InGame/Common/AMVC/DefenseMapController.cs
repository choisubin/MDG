using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DefenseMapController : BaseController
{
    [SerializeField]private DefenseMapBase _defenseMap;

    private List<UnitBase> _unitList = new List<UnitBase>();
    private List<AttackBase> _attackList = new List<AttackBase>();

    #region property
    private List<UnitBase> _unitHighHpSortList
    {
        get
        {
            return _unitList.OrderByDescending(a => a.CurrentHP).ThenByDescending(a => a.ArriveScore).ToList();
        }
    }
    private List<UnitBase> _unitLowHpSortList
    {
        get
        {
            return _unitList.OrderBy(a => a.CurrentHP).ThenByDescending(a => a.ArriveScore).ToList();
        }
    }

    private List<UnitBase> _unitHighArriveScoreSortList
    {
        get
        {
            return _unitList.OrderByDescending(a => a.ArriveScore).ThenBy(a => a.CurrentHP).ToList();
        }
    }

    private List<UnitBase> _unitLowArriveScoreSortList
    {
        get
        {
            return _unitList.OrderBy(a => a.ArriveScore).ThenBy(a => a.CurrentHP).ToList();
        }
    }
#endregion

    private Dictionary<int, UnitWrapperDefinition> _unitDefDic = new Dictionary<int, UnitWrapperDefinition>();

    public override void Init()
    {
        NotificationCenter.Instance.AddObserver(OnNotification, ENotiMessage.OnMatchBlock);
        _unitDefDic = DefinitionManager.Instance.GetDatas<UnitWrapperDefinition>();
    }

    public override void Set()
    {
        SetSpawnSchedule(2);
    }

    float _currentTime = 0;
    public override void AdvanceTime(float dt_sec)
    {
        _currentTime += dt_sec;

        //Unit관련
        RemoveEnemyToMap();
        SpawnScheduling(_currentTime);
        AdvanceUnits(dt_sec);

        //Attack관련
        RemoveAttackToMap();
        AdvanceAttack(dt_sec);
    }

    private AttackWrapper _tempAttackWrapper;
    public void OnNotification(Notification noti)
    {
        switch (noti.msg)
        {
            case ENotiMessage.OnMatchBlock:
                _tempAttackWrapper = (AttackWrapper)noti.data[EDataParamKey.AttackWrapper];
                SpawnAttack(_tempAttackWrapper);
                break;
        }
    }

    public override void Dispose()
    {
        NotificationCenter.Instance.RemoveObserver(OnNotification, ENotiMessage.OnMatchBlock);
    }


    #region Unit관련
    private List<Transform> _targetTrList = new List<Transform>();
    public List<Transform> GetTargetTransforms(EUnitTargetingType targetingType, int targetCount)
    {
        _targetTrList.Clear();
        int ctn = 0;
        switch (targetingType)
        {
            case EUnitTargetingType.Front:
                foreach (var unit in _unitHighArriveScoreSortList)
                {
                    if (unit.ArriveScore != 0)
                    {
                        ctn++;
                        _targetTrList.Add(unit.transform);
                    }
                    if (ctn >= targetCount)
                        return _targetTrList;
                }
                break;
            case EUnitTargetingType.HighHP:
                foreach (var unit in _unitHighHpSortList)
                {
                    ctn++;
                    _targetTrList.Add(unit.transform);
                    if (ctn >= targetCount)
                        return _targetTrList;
                }
                break;
            case EUnitTargetingType.LowHp:
                foreach (var unit in _unitLowHpSortList)
                {
                    ctn++;
                    _targetTrList.Add(unit.transform);
                    if (ctn >= targetCount)
                        return _targetTrList;
                }
                break;
            case EUnitTargetingType.Random:
            default:
                for (int i = 0; i < targetCount; i++)
                {
                    _targetTrList.Add(_unitList[Random.Range(0, _unitList.Count)].transform);
                }
                break;
        }
        return _targetTrList;
    }

    private void AdvanceUnits(float dt_sec)
    {
        foreach(var unit in _unitList)
        {
            unit.AdvanceTime(dt_sec);
        }
    }

    private List<StageEnemySpawnDefinition> _spawnDefList = new List<StageEnemySpawnDefinition>();
    private Dictionary<float, List<int>> _currentScheduleDic = new Dictionary<float, List<int>>();
    private void SetSpawnSchedule(int defKey)
    {
        _spawnDefList = DefinitionManager.Instance.GetData<List<StageEnemySpawnDefinition>>(defKey);
        foreach (var s in _spawnDefList)
        {
            float spawnTime = 0;
            for(int i = 0; i < s.SpawnUnitNum;i++)
            {
                spawnTime = s.Time + (s.SpawnCycle * i);
                if(!_currentScheduleDic.ContainsKey(spawnTime))
                {
                    _currentScheduleDic.Add(spawnTime, new List<int>());
                }
                _currentScheduleDic[spawnTime].Add(s.SpawnUnitKey);
            }
        }
    }

    private List<float> _scheduleRemoveList = new List<float>();
    private void SpawnScheduling(float _currentTime)
    {
        _scheduleRemoveList.Clear();
        foreach (var schedule in _currentScheduleDic)
        {
            if(_currentTime > schedule.Key)
            {
                _scheduleRemoveList.Add(schedule.Key);
                foreach(var unitKey in schedule.Value)
                {
                    SpawnEnemy(unitKey);
                }
            }
        }

        foreach (var remove in _scheduleRemoveList)
        {
            _currentScheduleDic.Remove(remove);
        }
    }

    private UnitWrapperDefinition _unitDef;
    private GameObject _tempUnitObj;
    private UnitBase _tempUnitBase;
    private void SpawnEnemy(int enemyKey)
    {
        _unitDef = _unitDefDic[enemyKey];
        _tempUnitObj = PoolManager.Instance.GrabPrefabs(EPrefabsType.Unit, _unitDef.PrefabsName, this.transform);
        _tempUnitBase = _tempUnitObj.GetComponent<UnitBase>();
        _tempUnitBase.Set(_unitDef, _defenseMap.Pos);
        _unitList.Add(_tempUnitBase);
    }

    private List<UnitBase> _unitBaseRemoveList = new List<UnitBase>();
    private void RemoveEnemyToMap()
    {
        _unitBaseRemoveList.Clear();
        foreach (var unit in _unitList)
        {
            if (!unit.IsAlive)
            {
                _unitBaseRemoveList.Add(unit);
            }
        }
        foreach (var remove in _unitBaseRemoveList)
        {
            _unitList.Remove(remove);
        }
    }

    #endregion

    #region attack관련

    private GameObject _tempAttackObj;
    private AttackBase _tempAttackBase;
    private void SpawnAttack(AttackWrapper wrapper)
    {
        if (_unitList.Count > 0)
        {
            List<Transform> trList = GetTargetTransforms(wrapper.unitTargetingType, wrapper.attackCount);
            foreach(var target in trList)
            {
                wrapper.targetEnemyTr = target;
                _tempAttackObj = PoolManager.Instance.GrabPrefabs(EPrefabsType.InGameAttack, wrapper.prefabsName);
                _tempAttackBase = _tempAttackObj.GetComponent<AttackBase>();
                _tempAttackBase.Set(wrapper);
                _attackList.Add(_tempAttackBase);
            }
        }
    }

    private List<Transform> _newTargetTr = new List<Transform>();
    private void AdvanceAttack(float dt_sec)
    {
        foreach (var attack in _attackList)
        {
            attack.AdvanceTime(dt_sec);
            if (attack.IsAlive && !attack.IsTargetEnable)//아직 살아있는데 타겟이 사라지면
            {
                if (_unitList.Count > 0)//남은 적이 있다면
                {
                    _newTargetTr.Clear();
                    _newTargetTr = GetTargetTransforms(attack.TargetType, 1);
                    if (_newTargetTr.Count >= 1)
                    {
                        attack.SetNewTarget(_newTargetTr[0]);
                    }
                }
                else //남은 적이 없다면 그냥 공격 캔슬
                {
                    attack.AttackCancle();
                }
            }
        }
    }

    private List<AttackBase> _attackBaseRemoveList = new List<AttackBase>();
    private void RemoveAttackToMap()
    {
        _attackBaseRemoveList.Clear();
        foreach (var attack in _attackList)
        {
            if (!attack.IsAlive)
            {
                _attackBaseRemoveList.Add(attack);
            }
        }
        foreach (var remove in _attackBaseRemoveList)
        {
            _attackList.Remove(remove);
        }
    }
    #endregion
}
