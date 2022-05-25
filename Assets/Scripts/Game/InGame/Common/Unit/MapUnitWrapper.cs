using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUnitWrapper
{
    public EUnitType unitType;
    public int indexX;
    public int indexY;

    public MapUnitWrapper(EUnitType unitType,int indexX,int indexY)
    {
        this.unitType = unitType;
        this.indexX = indexX;
        this.indexY = indexY;
    }
}
