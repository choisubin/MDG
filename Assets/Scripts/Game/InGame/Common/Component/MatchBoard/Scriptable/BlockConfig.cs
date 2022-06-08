using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Block Config", fileName = "BlockConfig.asset")]
public class BlockConfig : ScriptableObject
{
    public float[] dropSpeed;
    public Sprite[] basicBlockSprites;
    public Color[] blockColors;
    public GameObject explosion;

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
