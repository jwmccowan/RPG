using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifierFeature : Feature
{
    #region fields
    public StatTypes statType;
    public float value;

    CharacterSheet sheet { get { return target.GetComponentInParent<CharacterSheet>(); } }
    #endregion

    #region protected
    protected override void OnApply()
    {
        Stat stat = sheet.stats.GetStat<Stat>(statType);
        stat.statBaseValue += value;
    }

    protected override void OnRemove()
    {
        Stat stat = sheet.stats.GetStat<Stat>(statType);
        stat.statBaseValue -= value;
    }
    #endregion
}
