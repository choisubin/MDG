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
                return Instantiate(explosion) as GameObject;
            default:
                return Instantiate(explosion) as GameObject;
        }
    }

    public Color GetBlockColor(BlockBreed breed)
    {
        return blockColors[(int)breed];
    }
}
