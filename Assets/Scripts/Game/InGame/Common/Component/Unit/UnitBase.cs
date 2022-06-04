using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class UnitBase : MonoBehaviour
{
    [SerializeField]
    private EUnitType _unitType = EUnitType.TileMapUnit;

    [SerializeField]
    private int _curXIndex;
    [SerializeField]
    private int _curYIndex;
    public void Init(MapUnitWrapper wrapper)
    {
        _unitType = wrapper.unitType;
    }

    public void Set()
    {

    }

    public virtual void Move()
    {
        Debug.LogError("UnitBase");
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

}
