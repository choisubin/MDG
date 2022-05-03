using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MapEditor
{ 
    public class MapEditor_Tile : MonoBehaviour
    {
        [SerializeField]
        private EMapTile _type;
        [SerializeField]
        private float _itemSizeX = 128f;
        [SerializeField]
        private bool _isActive = false;
        public bool IsActive
        {
            get
            {
                return _isActive;
            }
        }

        private Image _img;

        private int _indexX;
        public void Init(int index)
        {
            _indexX = index;
            transform.localPosition = new Vector3(_indexX*_itemSizeX, transform.localPosition.y, transform.localPosition.z);
            _img = gameObject.GetComponent<Image>();
        }

        public void Set()
        {
        }

        public void OnBtnClick()
        {
            _isActive = !_isActive;

            if (_isActive) _img.color = new Color(0,0,0);
            else _img.color = new Color(1,1,1);
        }
    }
}

public enum EMapTile 
{
    STAGE1,
    STAGE2,
}
