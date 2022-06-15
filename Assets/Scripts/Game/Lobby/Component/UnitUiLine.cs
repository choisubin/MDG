using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitUiLine : MonoBehaviour
{
    [SerializeField] private UnitUiItem[] _unitItems;
    public void Set(int[] keys)
    {
        for(int i =0;i< _unitItems.Length;i++)
        {
            if (keys.Length<=i)
            {
                _unitItems[i].gameObject.SetActive(false);
            }
            else
            {
                _unitItems[i].Set(keys[i]);
            }
        }
    }
}
