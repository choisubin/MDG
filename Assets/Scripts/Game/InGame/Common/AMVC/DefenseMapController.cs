using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseMapController : BaseController
{
    [SerializeField]private DefenseMapBase _defenseMap;
    private List<UnitBase> _unitList = new List<UnitBase>();
    public override void Init()
    {
    }

    public override void Set()
    {
        foreach (var unit in _unitList)
        {
            unit.Set(DefinitionManager.Instance.GetData<UnitDefinition>(3), _defenseMap.Pos);
        }
    }

    float spawnTime = 0;
    public override void AdvanceTime(float dt_sec)
    {
        spawnTime += dt_sec;
        foreach(var unit in _unitList)
        {
            unit.AdvanceTime(dt_sec);
        }

        if(spawnTime > 1f)
        {
            GameObject go = PoolManager.Instance.GrabPrefabs(EPrefabsType.Unit, "Unit", this.transform);
            UnitBase ub = go.GetComponent<UnitBase>();
            _unitList.Add(ub);
            spawnTime = 0;
            ub.Set(DefinitionManager.Instance.GetData<UnitDefinition>(3), _defenseMap.Pos);
        }
    }

    public override void Dispose()
    {
    }
}
