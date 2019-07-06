using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEffectTarget : AbilityEffectTarget
{
    public override bool IsTarget(Tile tile)
    {
        if (tile == null || tile.content == null)
        {
            return false;
        }
        CharacterSheet sheet = tile.content.GetComponent<CharacterSheet>();
        return sheet != null && sheet.hp > 0;
    }
}
