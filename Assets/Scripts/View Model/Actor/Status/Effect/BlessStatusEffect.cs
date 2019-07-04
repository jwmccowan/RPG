using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessStatusEffect : StatusEffect
{
    CharacterSheet sheet;
    Bonus accuracyBonus;
    Bonus resolveBonus;

    void OnEnable()
    {
        sheet = GetComponentInParent<CharacterSheet>();
        // We can calculate bonus value when we get more info in character sheet
        Debug.Log(string.Format("Bless!! Previous ToHit: {0}.", sheet.stats[StatTypes.Stat_Accuracy]));
        accuracyBonus = new Bonus(StatTypes.Stat_Accuracy, BonusTypes.Morale, 1);
        resolveBonus = new Bonus(StatTypes.Stat_Resolve, BonusTypes.Morale, 1);
        sheet.AddBonus(accuracyBonus);
        sheet.AddBonus(resolveBonus);
        Debug.Log(string.Format("After ToHit: {0}.", sheet.stats[StatTypes.Stat_Accuracy]));
    }

    void OnDisable()
    {
        sheet.RemoveBonus(accuracyBonus);
        sheet.RemoveBonus(resolveBonus);
    }
}
