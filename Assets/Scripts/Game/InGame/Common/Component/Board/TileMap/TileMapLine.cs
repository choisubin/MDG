using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapLine : MonoBehaviour
{
    [SerializeField]
    private List<TileMapItem> _items = new List<TileMapItem>();
    public List<TileMapItem> Items
    {
        get
        {
            return _items;
        }
    }
    private int _lineIndex;

    private MapLineDefinition _lineDef = new MapLineDefinition();
    private float _lineSizeY = 128f;
    private int _maxWidthCount = 0;
    public void Init(MapLineDefinition linedef ,int maxWidthCount ,int lineIndex)
    {
        _lineDef = linedef;
        _maxWidthCount = maxWidthCount;
        _lineIndex = lineIndex;
        transform.localPosition = new Vector3(transform.localPosition.x, -(lineIndex * _lineSizeY), transform.localPosition.z);

        for(int i = 0; i < _maxWidthCount; i++)
        {
            GameObject go = PoolManager.Instance.GrabPrefabs(EPrefabsType.InGameBoard, "item_" + (int)_lineDef.mapItems[i].type, this.transform);
            _items.Add(go.GetComponent<TileMapItem>());
        }

        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].Init(_lineDef.mapItems[i],i);
        }
    }

    public void Set()
    {
    }
}
