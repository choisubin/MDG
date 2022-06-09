using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWrapper
{
    public float attackSpeed;
    public float baseAtk;
    public string prefabsName;
    public Transform startUnitTr;
    public Transform targetEnemyTr;

    public AttackWrapper(float attackSpeed, float baseAtk, string prefabsName, Transform startUnitTr, Transform targetEnemyTr)
    {
        this.attackSpeed = attackSpeed;
        this.baseAtk = baseAtk;
        this.prefabsName = prefabsName;
        this.startUnitTr = startUnitTr;
        this.targetEnemyTr = targetEnemyTr;
    }
}