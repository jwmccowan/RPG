﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantAbilityRange : AbilityRange
{
    public override List<Tile> GetTilesInRange(Board board)
    {
        return board.Search(unit.tile, ExpandSearch);
    }

    bool ExpandSearch(Tile from, Tile to)
    {
        return (from.distance + 1) <= horizontal && Mathf.Abs(to.height - from.height) <= vertical;
    }
}
