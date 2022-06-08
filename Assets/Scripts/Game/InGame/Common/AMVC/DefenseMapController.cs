using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseMapController : BaseController
{
    [SerializeField]private DefenseMapBase _defenseMap;

    private List<UnitBase> _unitList = new List<UnitBase>();

    private Dictionary<int, UnitDefinition> _unitDefDic = new Dictionary<int, UnitDefinition>();

    private Dictionary<float, List<int>> _currentScheduleDic = new Dictionary<float, List<int>>();

    public override void Init()
    {
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

        RemoveEnemyToMap();

        SpawnScheduling(_currentTime);

        AdvanceUnits(dt_sec);
    }

    public override void Dispose()
    {
    }

    #region private Method

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
}
