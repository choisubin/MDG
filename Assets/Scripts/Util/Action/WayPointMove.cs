using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMove
{
    private Transform[] _pos;
    private Transform _trObj;

    public float _speed;
    private int _num = 0;

    private bool _isArrive = false;
    public bool IsArrive
    {
        get
        {
            return _isArrive;
        }
    }

    private bool _isTargetEnable = true;
    public bool IsTargetEnable
    {
        get
        {
            return _isTargetEnable;
        }
    }

    public float ArriveScore //도착에 가까울 수록 높은 숫자 저장 (가까운 몬스터 구분 용)
    {
        get
        {
            if (_trObj == null || _pos.Length <= _num)
                return 0;
            return _num + (1 - Vector2.Distance(_trObj.transform.position, _pos[_num].transform.position) * 0.01f);
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

    public void Set(Transform trObj,Transform startPos, Transform endPos, float speed)
    {
        _isArrive = false;
        _trObj = trObj;
        _num = 0;
        _pos = new Transform[2];
        _pos[0] = startPos;
        _pos[1] = endPos;
        SetSpeed(speed);
        _trObj.position = _pos[_num].transform.position;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public void SetNewTarget(Transform tr)
    {
        _pos[1] = tr;
    }

    public void AdvanceTime(float dtTime)
    {
        MovePath();
    }

    private void MovePath()
    {
        if (_pos[1] != null)
        {
            if (_num == _pos.Length)
            {
                _isArrive = true;
                return;
            }

            _isTargetEnable = _pos[_num].gameObject.activeSelf;

            _trObj.position =
                Vector2.MoveTowards(_trObj.position, _pos[_num].transform.position, _speed * Time.deltaTime);

            if (_trObj.position == _pos[_num].transform.position)
                _num++;
        }
    }
}
