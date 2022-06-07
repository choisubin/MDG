using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameModel : BaseElement
{
    //현재 진행된 시간
    //ㄹㅇㄴㅁㄹㅇㄻㅇㄴㄹ
    [SerializeField]
    private float _time = 0;
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

    private int _stageNum = 0;

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

    private int _subStageNum = 0;
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

}
