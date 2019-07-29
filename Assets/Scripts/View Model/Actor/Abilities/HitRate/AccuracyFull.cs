using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyFull : Accuracy
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
