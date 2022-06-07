using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    private WayPointMove _moveComponent = new WayPointMove();

    public void Set(UnitDefinition def,Transform[] waypoints)
    {
        _moveComponent.Set(this.transform, waypoints, def.Speed);
        Debug.LogError(string.Format("{0} {1} {2} {3} {4} {5}", def.EUnitAttackEffect, def.EUnitAttackType, def.EUnitTargetingType, def.Speed, def.BaseAtk, def.BaseHp));
    }

    public void AdvanceTime(float dt_sec)
    {
        _moveComponent.AdvanceTime(dt_sec);
    }

}
