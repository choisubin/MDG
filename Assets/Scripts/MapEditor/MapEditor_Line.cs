using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MapEditor
{
    public class MapEditor_Line : MonoBehaviour
    {
        [SerializeField]
        private List<MapEditor_Tile> _items = new List<MapEditor_Tile>();
        public List<MapEditor_Tile> Items
        {
            get
            {
                return _items;
            }
        }
        [SerializeField]
        private float _lineSizeY = 128f;
        private int _lineIndex;

        public void Init(int lineIndex)
        {
            _lineIndex = lineIndex;
            transform.localPosition = new Vector3(transform.localPosition.x, -(lineIndex * _lineSizeY), transform.position.z) ;
            for (int i = 0; i <_items.Count;i++)
            {
                _items[i].Init(i);
            }
        }

        public bool isLineAlive()
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if(_items[i].IsActive)
                {
                    return true;
                }
            }
            return false;
        }
    }
}