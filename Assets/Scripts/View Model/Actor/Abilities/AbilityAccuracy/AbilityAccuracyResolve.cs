﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityAccuracyResolve : AbilityAccuracyDefense
{
    protected override int GetDefense(Unit unit)
    {
        return Mathf.FloorToInt(unit.GetComponent<CharacterSheet>().stats[StatTypes.Stat_Resolve]);
    }
}
