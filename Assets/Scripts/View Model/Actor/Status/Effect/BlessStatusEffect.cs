using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessStatusEffect : StatusEffect
{
    CharacterSheet sheet;
    StatModifierTotalAdd accuracyBonus;
    StatModifierTotalAdd resolveBonus;

    void OnEnable()
    {
        sheet = GetComponentInParent<CharacterSheet>();
        // We can calculate bonus value when we get more info in character sheet
        Debug.Log(string.Format("Bless!! Previous ToHit: {0}.", sheet.stats[StatTypes.Stat_Accuracy]));
        accuracyBonus = new StatModifierTotalAdd(1f, BonusTypes.Morale);
        resolveBonus = new StatModifierTotalAdd(1f, BonusTypes.Morale);
        sheet.stats.AddStatModifier(StatTypes.Stat_Accuracy, accuracyBonus);
        sheet.stats.AddStatModifier(StatTypes.Stat_Resolve, accuracyBonus);
        Debug.Log(string.Format("After ToHit: {0}.", sheet.stats[StatTypes.Stat_Accuracy]));
    }

    void OnDisable()
    {
        sheet.stats.RemoveStatModifier(StatTypes.Stat_Accuracy, accuracyBonus);
        sheet.stats.RemoveStatModifier(StatTypes.Stat_Resolve, accuracyBonus);
    }
}
