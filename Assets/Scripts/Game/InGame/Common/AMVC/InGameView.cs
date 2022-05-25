using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameView : BaseElement
{
    [SerializeField]
    private PuzzleMapComponent _puzzleMap;
    public PuzzleMapComponent PuzzleMap
    {
        get
        {
            return _puzzleMap;
        }
    }
}
