using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    private WayPointMove _moveComponent;
    [SerializeField] private Transform[] _pos;

    void Start()
    {
        _moveComponent = new WayPointMove();
        _moveComponent.Init(this.transform);
        Set();
    }

    public void Set()
    {
        _moveComponent.Set(_pos, 10f);
    }

    void Update()
    {
        _moveComponent.AdvanceTime(Time.deltaTime);
    }

}
