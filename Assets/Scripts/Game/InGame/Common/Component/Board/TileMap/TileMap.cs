using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    [SerializeField]
    private List<TileMapLine> _lines = new List<TileMapLine>();
    public List<TileMapLine> Lines
    {
        get
        {
            return _lines;
        }
    }
    private MapDefinition _mapDef = new MapDefinition();
    public MapDefinition MapDef
    {
        get
        {
            return _mapDef;
        }
    }
    private int _maxHeightCount=0;
    public int MaxHeight
    {
        get
        {
            return _maxHeightCount;
        }
    }
    private int _maxWidthCount=0;
    public int MaxWidth
    {
        get
        {
            return _maxWidthCount;
        }
    }
    const float _tileSize = 128f;
    const float _baseY = -192f;
    public void Init(StageDetailMapDefinition def)
    {

        _mapDef = def.mapGrid;
        _maxHeightCount = def.MaxHeightCount;
        _maxWidthCount = def.MaxWidthCount;
        for(int i = 0; i < _maxHeightCount; i ++)
        {
            GameObject go = PoolManager.Instance.GrabPrefabs(EPrefabsType.InGameTileMap, "itemLine", this.transform);
            _lines.Add(go.GetComponent<TileMapLine>());
        }

        for (int i = 0; i < _lines.Count; i++)
        {
            _lines[i].Init(_mapDef.mapLines[i] , _maxWidthCount ,i);
        }
        Vector2 v  = new Vector2(0,0);
        v.x = (_maxWidthCount / 2.0f) * _tileSize - (_tileSize / 2);
        v.y = (_maxHeightCount / 2.0f) * _tileSize - (_tileSize / 2) + _baseY;
        this.transform.position = new Vector2(this.transform.position.x - v.x , this.transform.position.y + v.y);
    }

    public void Set()
    {
    }
}
