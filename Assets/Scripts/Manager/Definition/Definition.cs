using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Definition : MonoBehaviour
{

}

[Serializable]
public class StageDetailBoardDefinition
{
    public int key;
    public int row;
    public int col;
    public int[] cells;
}

[Serializable]
public class StageDetailBoardDefinitionContainer : ILoader<int, StageDetailBoardDefinition>
{
    public List<StageDetailBoardDefinition> definitions = new List<StageDetailBoardDefinition>();
    public Dictionary<int, StageDetailBoardDefinition> MakeDict()
    {
        Dictionary<int, StageDetailBoardDefinition> dict = new Dictionary<int, StageDetailBoardDefinition>();
        foreach (StageDetailBoardDefinition definition in definitions)
        {
            dict.Add(definition.key, definition);
        }
        return dict;
    }
}


public class UnitWrapperDefinition
{
    public int key;
    public EUnitAttackType EUnitAttackType;
    public EUnitTargetingType EUnitTargetingType;
    public EUnitAttackEffect EUnitAttackEffect;
    public float Speed;
    public float BaseHp;
    public float BaseAtk;
    public float BaseAttackSpeed;
    public string PrefabsName;
    public string AttackPrefabsName;

    public UnitWrapperDefinition()
    {
    }

    public UnitWrapperDefinition(UnitDefinition def)
    {
        this.key = def.key;
        EUnitAttackType = (EUnitAttackType)Enum.Parse(typeof(EUnitAttackType), def.EUnitAttackType);
        EUnitTargetingType = (EUnitTargetingType)Enum.Parse(typeof(EUnitTargetingType), def.EUnitTargetingType);
        EUnitAttackEffect = (EUnitAttackEffect)Enum.Parse(typeof(EUnitAttackEffect), def.EUnitAttackEffect);
        Speed = def.Speed;
        BaseHp = def.BaseHp;
        BaseAtk = def.BaseAtk;
        BaseAttackSpeed = def.BaseAttackSpeed;
        PrefabsName = def.PrefabsName;
        AttackPrefabsName = def.AttackPrefabsName;
    }
}

[Serializable]
public class UnitDefinition
{
    public int key;
    public string EUnitAttackType;
    public string EUnitTargetingType;
    public string EUnitAttackEffect;
    public float Speed;
    public float BaseHp;
    public float BaseAtk;
    public float BaseAttackSpeed;
    public string PrefabsName;
    public string AttackPrefabsName;
}
[Serializable]
public class UnitDefinitionContainer : ILoader<int, UnitWrapperDefinition>
{
    public List<UnitDefinition> definitions = new List<UnitDefinition>();
    public Dictionary<int, UnitWrapperDefinition> MakeDict()
    {
        Dictionary<int, UnitWrapperDefinition> dict = new Dictionary<int, UnitWrapperDefinition>();
        foreach (UnitDefinition definition in definitions)
        {
            UnitWrapperDefinition wrapper = new UnitWrapperDefinition(definition);
            dict.Add(definition.key, wrapper);
        }
        return dict;
    }
}

[Serializable]
public class StageEnemySpawnDefinition
{
    public int key;
    public int StageNum;
    public int PartNum;
    public float Time;
    public int SpawnUnitKey;
    public int SpawnUnitNum;
    public float SpawnCycle;
}

[Serializable]
public class StageEnemySpawnDefinitionContainer : ILoader<int, List<StageEnemySpawnDefinition>>
{
    public List<StageEnemySpawnDefinition> definitions = new List<StageEnemySpawnDefinition>();
    public Dictionary<int, List<StageEnemySpawnDefinition>> MakeDict()
    {
        Dictionary<int, List<StageEnemySpawnDefinition>> dict = new Dictionary<int, List<StageEnemySpawnDefinition>>();
        foreach (StageEnemySpawnDefinition definition in definitions)
        {
            if (dict.ContainsKey(definition.key))
            {
                dict[definition.key].Add(definition);
            }
            else
            {
                dict.Add(definition.key, new List<StageEnemySpawnDefinition>());
                dict[definition.key].Add(definition);
            }
        }
        return dict;
    }
}
