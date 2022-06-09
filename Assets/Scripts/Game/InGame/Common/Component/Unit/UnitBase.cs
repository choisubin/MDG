using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    private UnitWrapperDefinition _unitDef;

    private bool _isAlive = true;
    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
    }
    private WayPointMove _moveComponent = new WayPointMove();

    public float MaxHp
    {
        get
        {
            return _unitDef.BaseHp;
        }
    }

    private float _currentHp;
    public float CurrentHP
    {
        set
        {
            if(value >= MaxHp)
            {
                CurrentHP = MaxHp;
            }
        }
        get
        {
            return _currentHp;
        }
    }

    public float ArriveScore
    {
        get
        {
            if (_moveComponent == null)
                return 0;
            return _moveComponent.ArriveScore;
        }
    }

    public void Set(UnitWrapperDefinition def,Transform[] waypoints)
    {
        _unitDef = def;
        _currentHp = MaxHp;
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
