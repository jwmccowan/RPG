using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRollHitRate : HitRate
{
    public override int Calculate(Tile tile)
    {
        if (AutomaticHit(tile))
        {
            return int.MinValue;
        }

        if (AutomaticMiss(tile))
        {
            return int.MaxValue;
        }


        return 0;
    }
}
