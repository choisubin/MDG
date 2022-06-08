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


[Serializable]
public class UnitDefinition
{
    public int key;
    public EUnitAttackType EUnitAttackType;
    public EUnitTargetingType EUnitTargetingType;
    public EUnitAttackEffect EUnitAttackEffect;
    public float Speed;
    public float BaseHp;
    public float BaseAtk;
    public string PrefabsName;
}

[Serializable]
public class UnitDefinitionContainer : ILoader<int, UnitDefinition>
{
    public List<UnitDefinition> definitions = new List<UnitDefinition>();
    public Dictionary<int, UnitDefinition> MakeDict()
    {
        Dictionary<int, UnitDefinition> dict = new Dictionary<int, UnitDefinition>();
        foreach (UnitDefinition definition in definitions)
        {
            dict.Add(definition.key, definition);
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
            Debug.LogError(definition.ToString());
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
