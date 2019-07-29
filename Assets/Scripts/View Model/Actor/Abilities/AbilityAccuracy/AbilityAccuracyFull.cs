using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityAccuracyFull : AbilityAccuracy
{
    public override int Calculate(Tile tile)
    {
        if (AutomaticMiss(tile))
        {
            return 0;
        }

        return int.MaxValue;
    }
}
