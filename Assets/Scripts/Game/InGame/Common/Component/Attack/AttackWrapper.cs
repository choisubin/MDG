using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWrapper:IngameElement
{
    public UnitWrapperDefinition unitDefinition;
    public List<InGameUpgradeUnitDefinition> inGameUpgradeUnitDefinitions = new List<InGameUpgradeUnitDefinition>();
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
    public int curEquipSlot;

    public float atkSpeed
    {
        get
        {
            float upgrade = 0;
            if(curEquipSlot!=-1)
            {
                int curLevel = app.model.SlotLevel[curEquipSlot];
                upgrade = inGameUpgradeUnitDefinitions[curLevel].AddAttackSpeed;
            }

            return unitDefinition.BaseAttackSpeed + upgrade;
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
            int upgrade = 0;
            if (curEquipSlot != -1)
            {
                int curLevel = app.model.SlotLevel[curEquipSlot];
                upgrade = inGameUpgradeUnitDefinitions[curLevel].AddAttackNum;
            }

            return unitDefinition.UnitAttackNum+ upgrade;
        }
    }

    public float atk
    {
        get
        {
            float upgrade = 0;
            if (curEquipSlot != -1)
            {
                int curLevel = app.model.SlotLevel[curEquipSlot];
                upgrade = inGameUpgradeUnitDefinitions[curLevel].AddAtk;
            }
            return unitDefinition.BaseAtk+ upgrade;
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

    private int GetEquipSlot()
    {
        FirebaseManager.UserUnitData[] u = FirebaseManager.Instance.CurrentEquipUnit;
        for (int i = 0;i< u.Length;i++)
        {
            if (u[i].key == unitDefinition.key)
            {
                return i;
            }
        }
        return -1;
    }

    public AttackWrapper(UnitWrapperDefinition unitDefinition, Transform startUnitTr)
    {
        this.unitDefinition = unitDefinition;
        this.startUnitTr = startUnitTr;
        curEquipSlot = GetEquipSlot();
        this.inGameUpgradeUnitDefinitions = DefinitionManager.Instance.GetData<List<InGameUpgradeUnitDefinition>>(unitDefinition.key);
    }
    
}