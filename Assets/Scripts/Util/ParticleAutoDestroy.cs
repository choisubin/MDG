using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleAutoDestroy : MonoBehaviour
{
    private EPrefabsType _ePaticleType = EPrefabsType.InGameMatchEffect;
    void OnEnable()
    {
        StartCoroutine(CoCheckAlive());
    }

    IEnumerator CoCheckAlive()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (!GetComponent<ParticleSystem>().IsAlive(true))
            {
                PoolManager.Instance.DespawnObject(_ePaticleType, gameObject);

                break;
            }
        }
    }
}
