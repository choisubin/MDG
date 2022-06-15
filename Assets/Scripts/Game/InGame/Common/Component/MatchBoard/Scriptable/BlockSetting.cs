using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BlockSetting : MonoBehaviour
{
    private Dictionary<int, UnitWrapperDefinition> _unitDefDic = new Dictionary<int, UnitWrapperDefinition>();
    public Dictionary<int, UnitWrapperDefinition> UnitDefDic
    {
        get
        {
            if(_unitDefDic==null)
            {
                _unitDefDic = DefinitionManager.Instance.GetDatas<UnitWrapperDefinition>();
            }
            return _unitDefDic;
        }
    }
    public float[] dropSpeed= { 0.3f,0.3f,0.3f,0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f };
    private Sprite[] _basicBlockSprites;
    public Sprite[] basicBlockSprites
    {
        get
        {
            if(_basicBlockSprites == null)
            {
                _basicBlockSprites = new Sprite[_unitDefDic.Count + 1];
                foreach (var key in _unitDefDic.Keys)
                {
                    basicBlockSprites[key] = Resources.Load<Sprite>("Sprites/Unit/" + _unitDefDic[key].UnitImageStr);
                }
            }
            return _basicBlockSprites;
        }
    }
    private Color[] _blockColors;
    public Color[] blockColors
    {
        get
        {
            if(_blockColors==null)
            {
                _blockColors = new Color[_unitDefDic.Count + 1];
                foreach (var key in _unitDefDic.Keys)
                {
                    blockColors[key] = new Color(1f, 1f, 1f);
                }
            }
            return _blockColors;
        }
    }

    public GameObject GetExplosionObject(BlockQuestType questType)
    {
        switch (questType)
        {
            case BlockQuestType.CLEAR_SIMPLE:
                return PoolManager.Instance.GrabPrefabs(EPrefabsType.InGameMatchEffect, "FX_BLOCK_EXPLOSION_NORMAL");
            default:
                return PoolManager.Instance.GrabPrefabs(EPrefabsType.InGameMatchEffect, "FX_BLOCK_EXPLOSION_NORMAL"); ;
        }
    }

    public Color GetBlockColor(int unitKey)
    {
        return blockColors[(int)unitKey];
    }
}
