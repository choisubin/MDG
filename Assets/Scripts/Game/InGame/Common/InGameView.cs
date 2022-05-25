using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameView : InGameElement
{
    [SerializeField]
    private TileMap _tileMap;
    public TileMap TileMap
    {
        get
        {
            return _tileMap;
        }
        set
        {
            _tileMap = value;
        }
    }
}
