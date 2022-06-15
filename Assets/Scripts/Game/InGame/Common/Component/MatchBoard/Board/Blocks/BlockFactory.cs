using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class BlockFactory
{
    public static Block SpawnBlock(BlockType blockType)
    {
        Block block = new Block(blockType);
        //Set Breed
        if (blockType == BlockType.BASIC)
        {
            int randomNum = UnityEngine.Random.Range(0, 5);
            block.unitKey = FirebaseManager.Instance.CurrentEquipUnit[randomNum].key;
        }
        else if (blockType == BlockType.EMPTY)
        {
            block.unitKey = 0;
        }

        return block;
    }
}
