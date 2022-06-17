using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DefenseMapBase : MonoBehaviour
{
    [SerializeField] private Transform[] _pos;
    [SerializeField] private TextMeshPro _hpTxt;
    public Transform[] Pos
    {
        get
        {
            return _pos;
        }
    }

    public TextMeshPro HpTxt
    {
        get
        {
            return _hpTxt;
        }
    }
}
