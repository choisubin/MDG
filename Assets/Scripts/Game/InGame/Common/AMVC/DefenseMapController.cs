using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseMapController : BaseController
{
    [SerializeField]private DefenseMapBase _defenseMap;

    private List<UnitBase> _unitList = new List<UnitBase>();
    private List<AttackBase> _attackList = new List<AttackBase>();

    private Dictionary<int, UnitDefinition> _unitDefDic = new Dictionary<int, UnitDefinition>();

    private Dictionary<float, List<int>> _currentScheduleDic = new Dictionary<float, List<int>>();

    public override void Init()
    {
        NotificationCenter.Instance.AddObserver(OnNotification, ENotiMessage.OnMatchBlock);
        _unitDefDic = DefinitionManager.Instance.GetDatas<UnitDefinition>();
    }

    public override void Set()
    {
        SetSpawnSchedule(1);
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
    private void AdvanceUnits(float dt_sec)
    {
        foreach(var unit in _unitList)
        {
            unit.AdvanceTime(dt_sec);
        }
    }

    private List<StageEnemySpawnDefinition> _spawnDefList = new List<StageEnemySpawnDefinition>();
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

    private UnitDefinition _unitDef;
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
        if(_unitList.Count>0)
            wrapper.targetEnemyTr = _unitList[0].transform;
        _tempAttackObj = PoolManager.Instance.GrabPrefabs(EPrefabsType.InGameAttack, wrapper.prefabsName);
        _tempAttackBase = _tempAttackObj.GetComponent<AttackBase>();
        _tempAttackBase.Set(wrapper);
        _attackList.Add(_tempAttackBase);
    }

    private void AdvanceAttack(float dt_sec)
    {
        foreach (var attack in _attackList)
        {
            attack.AdvanceTime(dt_sec);
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
