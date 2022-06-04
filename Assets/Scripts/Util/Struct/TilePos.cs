using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TilePos
{
    public int row { get; set; }
    public int col { get; set; }

    public TilePos(int nRow = 0, int nCol = 0)
    {
        row = nRow;
        col = nCol;
    }

    //----------------------------------------------------------------------
    // Struct 필수 override function
    //----------------------------------------------------------------------
    public override bool Equals(object obj)
    {
        return obj is TilePos pos && row == pos.row && col == pos.row;
    }

    public override int GetHashCode()
    {
        var hashCode = -928284752;
        hashCode = hashCode * -1521134295 + row.GetHashCode();
        hashCode = hashCode * -1521134295 + col.GetHashCode();
        return hashCode;
    }

    public override string ToString()
    {
        return $"(row = {row}, col = {col})";
    }
}
