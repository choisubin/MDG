using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameModel : IngameElement
{
    private float _gameTime = 0;
    public float GameTime
    {
        get
        {
            return _gameTime;
        }
        set
        {
            _gameTime = value;
        }
    }

    private int _money = 0;
    public int Money
    {
        get
        {
            return _money;
        }
        set
        {
            _money = value;
        }
    }

    private int _stageNum;
    public int StageNum
    {
        get
        {
            return _stageNum;
        }
    }

    private int _partNum;
    public int PartNum
    {
        get
        {
            return _partNum;
        }
    }

    public void SetStageInfo(int stage,int part)
    {
        _stageNum = stage;
        _partNum = part;
    }
}
