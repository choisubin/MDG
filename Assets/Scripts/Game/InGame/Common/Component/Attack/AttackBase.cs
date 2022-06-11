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

    public bool IsTargetEnable
    {
        get
        {
            return _moveComponent.IsTargetEnable;
        }
    }

    public EUnitTargetingType TargetType
    {
        get
        {
            return _attackWrapper.unitTargetingType;
        }
    }

    private WayPointMove _moveComponent = new WayPointMove();
    private AttackWrapper _attackWrapper;

    public void Set(AttackWrapper wrapper)
    {
        _isAlive = true;
        _attackWrapper = wrapper;
        _moveComponent.Set(this.transform, wrapper.startUnitTr, wrapper.targetEnemyTr, wrapper.atkSpeed);
    }

    public void AdvanceTime(float dt_sec)
    {
        _moveComponent.AdvanceTime(dt_sec);
        if (_moveComponent.IsArrive) //타겟에 무사히 도착 했을 때
        {
            _attackWrapper.AttackUnit();
            SpawnExplosionEffect();
            DespawnAttack();

        }
        //else if(!_moveComponent.IsTargetEnable) //타겟에 도착하지 않았는데 타겟 오브젝트가 비활성화 되었을 u
        //{
        //    PoolManager.Instance.DespawnObject(EPrefabsType.InGameAttack, this.gameObject);
        //    _isAlive = false;
        //}
    }

    public void SetNewTarget(Transform tr)
    {
        _moveComponent.SetNewTarget(tr);
    }

    public void DespawnAttack() //공격 취소
    {
        _isAlive = false;
        PoolManager.Instance.DespawnObject(EPrefabsType.InGameAttack, this.gameObject);
    }

    private void SpawnExplosionEffect()
    {
        GameObject explosionObj = PoolManager.Instance.GrabPrefabs(EPrefabsType.InGameMatchEffect, "FX_BLOCK_EXPLOSION_NORMAL");
        ParticleSystem.MainModule newModule = explosionObj.GetComponent<ParticleSystem>().main;

        explosionObj.transform.position = this.transform.position;
        explosionObj.SetActive(true);
    }
}
