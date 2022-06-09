using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBase : MonoBehaviour
{
    private bool _isAlive = true;
    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
    }

    private WayPointMove _moveComponent = new WayPointMove();
    private AttackWrapper _attackWrapper;

    public void Set(AttackWrapper wrapper)
    {
        _isAlive = true;
        _attackWrapper = wrapper;
        _moveComponent.Set(this.transform, wrapper.startUnitTr, wrapper.targetEnemyTr, wrapper.attackSpeed);
    }

    public void AdvanceTime(float dt_sec)
    {
        _moveComponent.AdvanceTime(dt_sec);
        if (_moveComponent.IsArrive)
        {
            PoolManager.Instance.DespawnObject(EPrefabsType.InGameAttack, this.gameObject);
            _isAlive = false;
        }
    }
}
