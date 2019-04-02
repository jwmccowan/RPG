using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAbilityArea : AbilityArea
{
    public override List<Tile> GetTilesInArea(Board board, Point pos)
    {
        List<Tile> retValue = new List<Tile>(1);
        Tile t = board.GetTile(pos);
        if (t != null)
        {
            retValue.Add(t);
        }
        return retValue;
    }
}
