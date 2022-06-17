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

    private int[] _slotLevel = {0,0,0,0,0};
    public int[] SlotLevel
    {
        get
        {
            return _slotLevel;
        }
        set
        {
            _slotLevel = value;
        }
    }

    private int _playerHP = 50;
    public int PlayerHP
    {
        get
        {
            return _playerHP;
        }
        set
        {
            if(value<=0)
            {
                _isAlive = false;
            }
            _playerHP = value;
        }
    }

    private bool _isAlive = true;
    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
        }
    }

    private int _stageSpawnKey;
    public int StageSpawnKey
    {
        get
        {
            return _stageSpawnKey;
        }
    }

    private bool _isClear = false;
    public bool IsClear
    {
        get
        {
            return _isClear;
        }
        set
        {
            _isClear = value;
        }
    }
    public void SetStageInfo(int stage,int part,int stageSpawnKey)
    {
        _stageNum = stage;
        _partNum = part;
        _stageSpawnKey = stageSpawnKey;
    }

    public void ResetAllModelValue()
    {
        int[] lev = { 0, 0, 0, 0, 0 };
        _slotLevel = lev;
        _money = 0;
        _gameTime = 0;
        _playerHP = 50;
        _isAlive = true;
        _isClear = false;
    }
}
