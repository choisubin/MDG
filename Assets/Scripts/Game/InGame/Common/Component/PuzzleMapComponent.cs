using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMapComponent : MonoBehaviour
{
    [SerializeField]
    private List<UnitBase> _mapUnits = new List<UnitBase>();
    [SerializeField]
    private TileMap _tileMap;
    [SerializeField]
    private Transform _trMapUnits;

    public void Init(StageDetailMapDefinition def)
    {
        if (def != null)
        {
            InitTileMap(def);
            InitPuzzleUnits();
        }
    }

    private void InitTileMap(StageDetailMapDefinition def)
    {
        _tileMap.Init(def);
    }

    private void InitPuzzleUnits()
    {
        List<TileMapItem> tileItems;
        GameObject goTemp;
        UnitBase unit;

        for (int h = 0; h < _tileMap.Lines.Count; h++)
        {
            tileItems = _tileMap.Lines[h].Items;
            for (int w = 0; w < tileItems.Count; w++)
            {
                goTemp = PoolManager.Instance.GrabPrefabs(EPrefabsType.Unit, "Unit", _trMapUnits);
                unit = goTemp.GetComponent<UnitBase>();
                unit.SetPosition(tileItems[w].transform.position);
                unit.Init(new MapUnitWrapper(EUnitType.TileMapUnit, w, h));
                _mapUnits.Add(unit);
            }
        }
    }
}
