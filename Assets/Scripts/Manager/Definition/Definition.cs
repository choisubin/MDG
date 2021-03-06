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
    public EUnitTargetingType EUnitTargetingType;
    public EUnitAttackEffect EUnitAttackEffect;
    public EUnitAttackType EUnitAttackType;
    public int UnitAttackNum;
    public float Speed;
    public float BaseHp;
    public float BaseAtk;
    public float BaseAttackSpeed;
    public string PrefabsName;
    public string AttackPrefabsName;
    public string UnitImageStr;
    public int RewardCoin;

    public UnitWrapperDefinition()
    {
    }

    public UnitWrapperDefinition(UnitDefinition def)
    {
        this.key = def.key;
        EUnitAttackType = (EUnitAttackType)Enum.Parse(typeof(EUnitAttackType), def.EUnitAttackType);
        EUnitTargetingType = (EUnitTargetingType)Enum.Parse(typeof(EUnitTargetingType), def.EUnitTargetingType);
        EUnitAttackEffect = (EUnitAttackEffect)Enum.Parse(typeof(EUnitAttackEffect), def.EUnitAttackEffect);
        UnitAttackNum = def.UnitAttackNum;
        Speed = def.Speed;
        BaseHp = def.BaseHp;
        BaseAtk = def.BaseAtk;
        BaseAttackSpeed = def.BaseAttackSpeed;
        PrefabsName = def.PrefabsName;
        AttackPrefabsName = def.AttackPrefabsName;
        UnitImageStr = def.UnitImageStr;
        RewardCoin = def.RewardCoin;
    }
}

[Serializable]
public class UnitDefinition
{
    public int key;
    public string EUnitTargetingType;
    public string EUnitAttackEffect;
    public string EUnitAttackType;
    public int UnitAttackNum;
    public float Speed;
    public float BaseHp;
    public float BaseAtk;
    public float BaseAttackSpeed;
    public string PrefabsName;
    public string AttackPrefabsName;
    public string UnitImageStr;
    public int RewardCoin;
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

[Serializable]
public class StageDefinition
{
    public int key;
    public int PartNum;
    public int BoardKey;
    public int MonsterSpawnKey;
    public string TitleUnitImgStr;
    public string DefenseMapPrefabName;
}

[Serializable]
public class StageDefinitionContainer : ILoader<int, List<StageWrapperDefinition>>
{
    public List<StageDefinition> definitions = new List<StageDefinition>();
    public Dictionary<int, List<StageWrapperDefinition>> MakeDict()
    {
        Dictionary<int, List<StageWrapperDefinition>> dict = new Dictionary<int, List<StageWrapperDefinition>>();
        foreach (StageDefinition definition in definitions)
        {
            if (dict.ContainsKey(definition.key))
            {
                dict[definition.key].Add(new StageWrapperDefinition(definition));
            }
            else
            {
                dict.Add(definition.key, new List<StageWrapperDefinition>());
                dict[definition.key].Add(new StageWrapperDefinition(definition));
            }
        }
        return dict;
    }
}

public class StageWrapperDefinition
{
    public int key;
    public int partNum;
    public int boardKey;
    public int monsterSpawnKey;
    public string titleUnitImgStr;
    public string defenseMapPrefabName;
    public List<StageEnemySpawnDefinition> enemyspawnDef = new List<StageEnemySpawnDefinition>();
    public StageDetailBoardDefinition boardDef = new StageDetailBoardDefinition();
    public StageWrapperDefinition()
    {
    }

    public StageWrapperDefinition(StageDefinition def)
    {
        this.key = def.key;
        partNum = def.PartNum;
        boardKey = def.BoardKey;
        monsterSpawnKey = def.MonsterSpawnKey;
        titleUnitImgStr = def.TitleUnitImgStr;
        defenseMapPrefabName = def.DefenseMapPrefabName;
        enemyspawnDef = DefinitionManager.Instance.GetData<List<StageEnemySpawnDefinition>>(monsterSpawnKey);
        boardDef = DefinitionManager.Instance.GetData<StageDetailBoardDefinition>(boardKey);
    }
}

[Serializable]
public class InGameUpgradeUnitDefinition
{
    public int key;
    public int Level;
    public int AddAttackNum;
    public float AddAtk;
    public float AddAttackSpeed;
    public int nextUpgradeCoin;
}

[Serializable]
public class InGameUpgradeUnitDefinitionContainer : ILoader<int, List<InGameUpgradeUnitDefinition>>
{
    public List<InGameUpgradeUnitDefinition> definitions = new List<InGameUpgradeUnitDefinition>();
    public Dictionary<int, List<InGameUpgradeUnitDefinition>> MakeDict()
    {
        Dictionary<int, List<InGameUpgradeUnitDefinition>> dict = new Dictionary<int, List<InGameUpgradeUnitDefinition>>();
        foreach (InGameUpgradeUnitDefinition definition in definitions)
        {
            if (dict.ContainsKey(definition.key))
            {
                dict[definition.key].Add(definition);
            }
            else
            {
                dict.Add(definition.key, new List<InGameUpgradeUnitDefinition>());
                dict[definition.key].Add(definition);
            }
        }
        return dict;
    }
}