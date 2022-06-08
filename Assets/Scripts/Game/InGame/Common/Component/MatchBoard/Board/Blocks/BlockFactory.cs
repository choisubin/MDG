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
            block.unitKey = UnityEngine.Random.Range(1, 5);
        else if (blockType == BlockType.EMPTY)
            block.unitKey = 0;

        return block;
    }
}
