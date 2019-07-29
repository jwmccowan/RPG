using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyDefense : Accuracy
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

        Unit defender = tile.content.GetComponent<Unit>();
        
        int accuracy = GetAccuracy(unit) - GetDefense(defender) + accuracyBonus;

        accuracy = AdjustForStatusEffects(tile, accuracy);
        accuracy = AdjustForPositioning(tile, accuracy);

        return accuracy;
    }

    protected virtual int GetDefense(Unit unit)
    {
        return 0;
    }

    protected int GetAccuracy(Unit unit)
    {
        return Mathf.FloorToInt(unit.GetComponent<CharacterSheet>().stats[StatTypes.Stat_Deflection]);
    }
}
