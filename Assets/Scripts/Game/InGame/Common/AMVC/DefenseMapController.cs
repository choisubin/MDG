using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseMapController : BaseController
{
    [SerializeField]private DefenseMapBase _defenseMap;
    private List<UnitBase> _unitList = new List<UnitBase>();
    private Dictionary<int, UnitDefinition> _unitDefDic = new Dictionary<int, UnitDefinition>();
    public override void Init()
    {
        _unitDefDic = DefinitionManager.Instance.GetDatas<UnitDefinition>();
    }

    public override void Set()
    {
        List<StageEnemySpawnDefinition> spawndef = DefinitionManager.Instance.GetData<List<StageEnemySpawnDefinition>>(1);
        foreach(var s in spawndef)
        {
            Debug.LogError(string.Format("{0} {1} {2} {3}", s.SpawnUnitKey, s.SpawnUnitNum, s.SpawnCycle, s.SpawnUnitNum));
        }
    }

    float spawnTime = 0;
    public override void AdvanceTime(float dt_sec)
    {
        spawnTime += dt_sec;

        RemoveEnemyToMap();

        if (spawnTime > 0.1f)
        {
            SpawnEnemy(Random.Range(1, 9));
            spawnTime = 0;
        }
    }

    public override void Dispose()
    {
    }

    #region private Method
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

    private List<UnitBase> _removeList = new List<UnitBase>();
    private void RemoveEnemyToMap()
    {
        _removeList.Clear();
        foreach (var unit in _unitList)
        {
            if (!unit.IsAlive)
            {
                _removeList.Add(unit);
            }
        }
        foreach (var remove in _removeList)
        {
            _unitList.Remove(remove);
        }
    }
    #endregion
}
