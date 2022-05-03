using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapEditor
{

    public class MapEditor_Page : MonoBehaviour
    {
        [SerializeField]
        private List<MapEditor_Line> _lines = new List<MapEditor_Line>();
        public List<MapEditor_Line> Lines
        {
            get
            {
                return _lines;
            }
        }
        public void Init()
        {
            for (int i = 0; i < _lines.Count; i++)
            {
                _lines[i].Init(i);
            }
        }
    }
}