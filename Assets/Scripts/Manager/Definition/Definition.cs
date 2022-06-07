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