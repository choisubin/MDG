using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private UnitUiLine[] _unitUiLines;
    [SerializeField] private UnitUiLine _equipUnitLines;
    public void Set()
    {
        SetUnitUiLine();

    }

    public void SetUnitUiLine()
    {
        int count = 1;
        int linecount = 0;
        List<int> haveUnitKeys = new List<int>();
        int[] equipUnitKeys = new int[5];
        foreach (var dataDic in FirebaseManager.Instance.userUnitDataDic)
        {
            haveUnitKeys.Add(dataDic.Key);
            int slot = dataDic.Value.equipSlot;
            if (slot>0&&slot<6)
            {
                equipUnitKeys[slot-1] = dataDic.Key;
            }
            if (count % 4 == 0)
            {
                _unitUiLines[linecount++].Set(haveUnitKeys.ToArray());
                haveUnitKeys.Clear();
            }
            count++;
        }
        if (haveUnitKeys.Count > 0)
        {
            _unitUiLines[linecount++].Set(haveUnitKeys.ToArray());
        }
        _equipUnitLines.Set(equipUnitKeys);
    }
}
