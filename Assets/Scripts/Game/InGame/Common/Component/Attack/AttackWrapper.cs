using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWrapper
{
    public UnitWrapperDefinition unitDefinition;
    public Transform startUnitTr;
    public Transform targetEnemyTr;

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

    public EUnitTargetingType unitTargetingType
    {
        get
        {
            return unitDefinition.EUnitTargetingType;
        }
    }

    public AttackWrapper(UnitWrapperDefinition unitDefinition, Transform startUnitTr, Transform targetEnemyTr)
    {
        this.unitDefinition = unitDefinition;
        this.startUnitTr = startUnitTr;
        this.targetEnemyTr = targetEnemyTr;
    }
    
}