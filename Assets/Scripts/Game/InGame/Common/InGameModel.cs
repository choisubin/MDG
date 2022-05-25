using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameModel : InGameElement
{
    //현재 진행된 시간
    //ㄹㅇㄴㅁㄹㅇㄻㅇㄴㄹ
    [SerializeField]
    private float _time =0;
    public float time
    {
        get
        {
            return _time;
        }
        set
        {
            _time = value;
        }
    }

    private int _stageNum;

    public int StageNum
    {
        get
        {
            return _stageNum;
        }
        set
        {
            _stageNum = value;
        }
    }

    private int _subStageNum;
    public int SubStageNum
    {
        get
        {
            return _subStageNum;
        }
        set
        {
            _subStageNum = value;
        }
    }
    private int _mapKey = 0;
    public int MapKey
    {
        get
        {
            return _mapKey;
        }
        set
        {
            _mapKey = value;
        }
    }

    private StageDetailMapDefinition _mapDef;
    public StageDetailMapDefinition MapDef
    {
        get
        {
            if (_mapDef == null)
            {
                _mapDef = DefinitionManager.Instance.GetData<StageDetailMapDefinition>(_mapKey);  //나중에 key값으로 대체해야함
            }
            return _mapDef;
        }
    }
}
