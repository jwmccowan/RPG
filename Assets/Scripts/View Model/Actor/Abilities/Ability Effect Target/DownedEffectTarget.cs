using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownedEffectTarget : AbilityEffectTarget
{
    public override bool IsTarget(Tile tile)
    {
        if (tile == null || tile.content == null)
        {
            return false;
        }

        Stats s = tile.content.GetComponent<Stats>();
        //TOREMOVE: this should be current hp not max hp
        return s != null && s[StatTypes.Stat_Max_HP] <= 0;
    }
}
