using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWrapper
{
    public UnitDefinition unitDefinition;
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

    public AttackWrapper(UnitDefinition unitDefinition, Transform startUnitTr, Transform targetEnemyTr)
    {
        this.unitDefinition = unitDefinition;
        this.startUnitTr = startUnitTr;
        this.targetEnemyTr = targetEnemyTr;
    }
    
}