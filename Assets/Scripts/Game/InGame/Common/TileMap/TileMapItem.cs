using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapItem : MonoBehaviour
{
    [SerializeField]
    private EMapTile _type;
    [SerializeField]
    private int _indexX;
    [SerializeField]
    private Sprite _sprTile;

    private MapItemDefinition _itemDef = new MapItemDefinition();
    private float _itemSizeX = 128f;

    public void Init(MapItemDefinition itemDef,int index)
    {
        _itemDef = itemDef;
        _indexX = index;
        transform.localPosition = new Vector3(_indexX * _itemSizeX, 0, 0);

        this.gameObject.SetActive(itemDef.isActive);
    }

    public void Set()
    {

    }
}
