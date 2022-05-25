using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Definition : MonoBehaviour
{

}

[Serializable]
public class StageDefinition
{
    public int key;
    public StageDetailDefinition[] detailStage;
}

[Serializable]
public class StageDetailDefinition
{
    public int key;
    public int stageNum;
    public int semiStageNum;
    public int stageDetailMapKey;
}

[Serializable]
public class StageDetailMapDefinition
{
    public int key;
    public int MaxWidthCount;
    public int MaxHeightCount;
    public MapDefinition mapGrid = new MapDefinition();
}


[System.Serializable]
public class MapDefinition
{
    public List<MapLineDefinition> mapLines = new List<MapLineDefinition>();
}

[System.Serializable]
public class MapLineDefinition
{
    public List<MapItemDefinition> mapItems = new List<MapItemDefinition>();
}

[System.Serializable]
public class MapItemDefinition
{
    public bool isActive;
    public EMapTile type;
}

[Serializable]
public class StageDetailMapDefinitionContainer : ILoader<int, StageDetailMapDefinition>
{
    public List<StageDetailMapDefinition> definitions = new List<StageDetailMapDefinition>();
    public Dictionary<int, StageDetailMapDefinition> MakeDict()
    {
        Dictionary<int, StageDetailMapDefinition> dict = new Dictionary<int, StageDetailMapDefinition>();
        foreach (StageDetailMapDefinition definition in definitions)
        {
            dict.Add(definition.key, definition);
        }
        return dict;
    }
}
