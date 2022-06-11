using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWrapper
{
    public UnitWrapperDefinition unitDefinition;
    public Transform startUnitTr;

    private Transform _targetEnemyTr;
    public Transform targetEnemyTr
    {
        get
        {
            return _targetEnemyTr;
        }
        set
        {
            if (value != null)
            {
                _targetEnemyTr = value;
                targetUnit = _targetEnemyTr.GetComponent<UnitBase>();
            }
        }
    }
    public UnitBase targetUnit;

    public float atkSpeed
    {
        get
        {
            return unitDefinition.BaseAttackSpeed;
        }
    }
    public string prefabsName
    {
        get
        {
            return unitDefinition.AttackPrefabsName;
        }
    }

    public int attackCount
    {
        get
        {
            return unitDefinition.UnitAttackNum;
        }
    }

    public float atk
    {
        get
        {
            return unitDefinition.BaseAtk;
        }
    }

    public EUnitTargetingType unitTargetingType
    {
        get
        {
            return unitDefinition.EUnitTargetingType;
        }
    }

    public void AttackUnit()
    {
        if (targetUnit != null)
        {
            targetUnit.CurrentHP -= atk;
        }
        else
        {
            Debug.LogError("_targetUnit is null");
        }
    }

    public AttackWrapper(UnitWrapperDefinition unitDefinition, Transform startUnitTr)
    {
        this.unitDefinition = unitDefinition;
        this.startUnitTr = startUnitTr;
    }
    
}