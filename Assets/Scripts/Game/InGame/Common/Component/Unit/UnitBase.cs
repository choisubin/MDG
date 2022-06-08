using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    private bool _isAlive = true;
    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
    }
    private WayPointMove _moveComponent = new WayPointMove();

    public void Set(UnitDefinition def,Transform[] waypoints)
    {
        _isAlive = true;
        _moveComponent.Set(this.transform, waypoints, def.Speed);
        //Debug.LogError(string.Format("{0} {1} {2} {3} {4} {5}", def.EUnitAttackEffect, def.EUnitAttackType, def.EUnitTargetingType, def.Speed, def.BaseAtk, def.BaseHp));
    }

    public void AdvanceTime(float dt_sec)
    {
        _moveComponent.AdvanceTime(dt_sec);
        if(_moveComponent.IsArrive)
        {
            PoolManager.Instance.DespawnObject(EPrefabsType.Unit, this.gameObject);
            _isAlive = false;
        }
    }

}
