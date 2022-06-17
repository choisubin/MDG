﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}


public class DefinitionManager : MonoBehaviour 
{
    #region Singelton
    private static DefinitionManager _instance;
    public static DefinitionManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DefinitionManager>();
                if (FindObjectsOfType<DefinitionManager>().Length > 1)
                {
                    Debug.LogError("[Singleton] Something went really wrong " +
                        " - there should never be more than 1 singleton!" +
                        " Reopening the scene might fix it.");
                    return _instance;
                }

                if (_instance == null)
                {
                    GameObject go = new GameObject("DefinitionManager");
                    _instance = go.AddComponent<DefinitionManager>();
                }
            }

            return _instance;
        }
    }
    #endregion

    public void Awake()
    {
        LoadAllJson();
    }

    Hashtable _definitions = new Hashtable();
    private void LoadAllJson()
    {
        LoadJson<StageDetailBoardDefinitionContainer,StageDetailBoardDefinition>("Stage/StageDetailBoardDefinition");
        LoadJson<UnitDefinitionContainer, UnitWrapperDefinition>("Unit/UnitDefinition");
        LoadJson<StageEnemySpawnDefinitionContainer, List<StageEnemySpawnDefinition>>("Stage/StageMonsterSpawnDefinition");
        LoadJson<StageDefinitionContainer, List<StageWrapperDefinition>>("Stage/StageDefinition");
        LoadJson<InGameUpgradeUnitDefinitionContainer, List<InGameUpgradeUnitDefinition>>("Unit/InGameUpgradeUnitDefinition");
    }

    private void LoadJson<ContainerType, DefType>(string path) where ContainerType : ILoader<int, DefType>
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Definition/" +path);
        _definitions[typeof(DefType)] = JsonUtility.FromJson<ContainerType>(textAsset.text).MakeDict();
    }

    public Dictionary<int, T> GetDatas<T>()
    {
        if(_definitions.ContainsKey(typeof(T)))
        {
            return _definitions[typeof(T)] as Dictionary<int, T>;
        }
        return null;
    }
    public T GetData<T>(int key)
    {
        if (_definitions.ContainsKey(typeof(T)))
        {
            Dictionary<int, T> definition = _definitions[typeof(T)] as Dictionary<int, T>;
            return definition[key];
        }
        return default(T);
    }
}
