using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyDeflection : AccuracyDefense
{
    protected override int GetDefense(Unit unit)
    {
        return Mathf.FloorToInt(unit.GetComponent<CharacterSheet>().stats[StatTypes.Stat_Deflection]);
    }
}
