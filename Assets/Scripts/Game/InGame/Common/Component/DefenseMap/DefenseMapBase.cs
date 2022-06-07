using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseMapBase : MonoBehaviour
{
    [SerializeField] private Transform[] _pos;
    public Transform[] Pos
    {
        get
        {
            return _pos;
        }
    }
}
