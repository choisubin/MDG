using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMove
{
    private Transform[] _pos;
    private float _speed = 5f;
    private Transform _trObj;
    private int _num = 0;
    private bool _isArrive = false;
    public bool IsArrive
    {
        get
        {
            return _isArrive;
        }
    }

    public void Set(Transform trObj, Transform[] pos,float speed)
    {
        _isArrive = false;
        _trObj = trObj;
        _num = 0;
        _pos = pos;
        SetSpeed(speed);
        if (pos.Length > 0)
        {
            _trObj.position = _pos[_num].transform.position;
        }
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public void AdvanceTime(float dtTime)
    {
        MovePath();
    }

    private void MovePath()
    {
        if (_num == _pos.Length)
        {
            _isArrive = true;
            return;
        }

        _trObj.position =
            Vector2.MoveTowards(_trObj.position, _pos[_num].transform.position, _speed * Time.deltaTime);

        if (_trObj.position == _pos[_num].transform.position)
            _num++;
    }
}
