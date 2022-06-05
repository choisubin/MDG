using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    private int _objLife;
    public int ObjLife
    {
        get
        {
            return _objLife;
        }
    }

    private string _name;
    public string Name
    {
        get
        {
            return _name;
        }
    }
    private bool _isEnable;
    private Transform _poolLayer;


    public void SetData(string name)
    {
        _name = name;
    }

    public void EnableObject(Transform parent)
    {
        _objLife = 3;
        _isEnable = true;

        this.gameObject.SetActive(true);
        this.transform.SetParent(parent);
    }

    public void EnableObject()
    {
        _objLife = 3;
        _isEnable = true;

        this.gameObject.SetActive(true);
    }

    private Vector3 _baseVecTo0 = new Vector3(0, 0, 0);
    private Vector3 _baseVecTo1 = new Vector3(1, 1, 1);
    private Quaternion _baseQuat = Quaternion.identity;
    public void DisableObject(Transform pool)
    {
        transform.localScale = _baseVecTo1;
        transform.SetPositionAndRotation(_baseVecTo0, _baseQuat);

        _isEnable = false;
        this.gameObject.SetActive(false);
        this.transform.SetParent(pool);
    }

    public bool IsDie()
    {
        if(--_objLife > 0)
        {
            return false;
        }
        return true;
    }
}
