using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace MapEditor
{
    public class MapEditorManager : MonoBehaviour
    {
        [SerializeField]
        private MapEditor_Page _page;
        [SerializeField]
        private int _key;
        void Start()
        {
            _page.Init();
        }

        // Update is called once per frame
        void Update()
        {

        }

        //public void OnClickConvertToJson()
        //{
        //    StageDetailMapDefinition sdm = new StageDetailMapDefinition();
        //    MapDefinition mapFile = new MapDefinition();
        //    sdm.MaxWidthCount = 0;
        //    sdm.MaxHeightCount = 0;
        //    for (int h = 0; h < _page.Lines.Count; h++)
        //    {
        //        mapFile.mapLines.Add(new MapLineDefinition());
        //        Debug.LogError(_page.Lines[h].isLineAlive());
        //        if (_page.Lines[h].isLineAlive())
        //        {
        //            if (h + 1 > sdm.MaxHeightCount)
        //            {
        //                sdm.MaxHeightCount = h + 1;
        //            }
        //        }

        //        for (int w = 0; w < _page.Lines[h].Items.Count; w++)
        //        {
        //            MapEditor_Tile tile = _page.Lines[h].Items[w];
        //            MapItemDefinition item = new MapItemDefinition();
        //            item.isActive = tile.IsActive;
        //            item.type = tile.Type;
        //            mapFile.mapLines[h].mapItems.Add(item);
        //            if(tile.IsActive)
        //            {
        //                if (w+1 > sdm.MaxWidthCount)
        //                {
        //                    sdm.MaxWidthCount = w + 1;
        //                }
        //            }
        //        }
        //    }
        //    sdm.key = _key;
        //    sdm.mapGrid = mapFile;

        //    string json = JsonUtility.ToJson(sdm);
        //    string path = Path.Combine(Application.dataPath+"/Resources/Definition/Temp", "StageDetailMapDefinition"+_key.ToString()+ ".json");
        //    File.WriteAllText(path, json);
        //}
    }

}