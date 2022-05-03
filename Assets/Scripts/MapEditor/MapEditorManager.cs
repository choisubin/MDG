using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MapEditor
{
    public class MapEditorManager : MonoBehaviour
    {
        [SerializeField]
        private MapEditor_Page _page;
        void Start()
        {
            _page.Init();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnClickConvertToJson()
        {
            Map mapFile = new Map();
            for(int h = 0;h <  _page.Lines.Count; h++)
            {
                mapFile.map.Add(new List<int>());
                for (int w = 0; w < _page.Lines[h].Items.Count;w++)
                {
                    mapFile.map[h].Add(_page.Lines[h].Items[w].IsActive ? 1 : 0);
                }
            }
            string json = JsonUtility.ToJson(mapFile);
            Debug.Log(json);
        }
    }

    [System.Serializable]
    public class Map
    {
        public List<List<int>> map = new List<List<int>>();
    }
}