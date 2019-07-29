using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessStatusEffect : StatusEffect
{
    CharacterSheet sheet;
    Bonus toHitBonus;
    Bonus willBonus;

    void OnEnable()
    {
        sheet = GetComponentInParent<CharacterSheet>();
        // We can calculate bonus value when we get more info in character sheet
        Debug.Log(string.Format("Bless!! Previous ToHit: {0}.", sheet.stats[StatTypes.To_Hit]));
        toHitBonus = new Bonus(StatTypes.To_Hit, BonusTypes.Morale, 1);
        willBonus = new Bonus(StatTypes.Will, BonusTypes.Morale, 1);
        sheet.AddBonus(toHitBonus);
        sheet.AddBonus(willBonus);
        Debug.Log(string.Format("After ToHit: {0}.", sheet.stats[StatTypes.To_Hit]));
    }

    void OnDisable()
    {
        sheet.RemoveBonus(toHitBonus);
        sheet.RemoveBonus(willBonus);
    }
}
