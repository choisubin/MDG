using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    private UnitWrapperDefinition _unitDef;

    private bool _isAlive = true;
    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
    }
    private WayPointMove _moveComponent = new WayPointMove();

    public float MaxHp
    {
        get
        {
            return _unitDef.BaseHp;
        }
    }

    private float _currentHp;
    public float CurrentHP
    {
        set
        {
            if(value < 0)
            {
                _currentHp = 0;
                Hashtable sendData = new Hashtable();
                sendData.Add(EDataParamKey.Integer, _unitDef.RewardCoin);
                NotificationCenter.Instance.PostNotification(ENotiMessage.OnKillEnemy, sendData);
            }
            else
            {
                _currentHp = Mathf.Min(value ,MaxHp);
                float scale = Mathf.Max(_currentHp / MaxHp,0.1f);
                transform.localScale = new Vector2(scale, scale);
            }
        }
        get
        {
            return _currentHp;
        }
    }

    public float ArriveScore
    {
        get
        {
            if (_moveComponent == null)
                return 0;
            return _moveComponent.ArriveScore;
        }
    }

    public void Set(UnitWrapperDefinition def,Transform[] waypoints)
    {
        _unitDef = def;
        CurrentHP = MaxHp;
        _isAlive = true;
        _moveComponent.Set(this.transform, waypoints, def.Speed);
    }

    public void AdvanceTime(float dt_sec)
    {
        _moveComponent.AdvanceTime(dt_sec);
        if(_moveComponent.IsArrive || CurrentHP == 0)
        {
            DespawnUnit();
        }
    }

    private void DespawnUnit()
    {
        _isAlive = false;
        PoolManager.Instance.DespawnObject(EPrefabsType.Unit, this.gameObject);
    }
}
