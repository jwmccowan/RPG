﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfAbilityRange : AbilityRange
{
    public override List<Tile> GetTilesInRange(Board board)
    {
        List<Tile> list = new List<Tile>(1)
        {
            unit.tile
        };
        return list;
    }
}
