using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBonusFeature : Feature
{
    #region fields
    public StatModifier modifier;
    public StatTypes stat;

    CharacterSheet sheet { get { return target.GetComponentInParent<CharacterSheet>(); } }
    #endregion

    #region public
    public void SetModifier(StatTypes stat, StatModifier modifier)
    {
        this.stat = stat;
        this.modifier = modifier;
    }
    #endregion

    #region protected
    protected override void OnApply()
    {
        sheet.stats.AddStatModifier(stat, modifier);
    }

    protected override void OnRemove()
    {
        sheet.stats.RemoveStatModifier(stat, modifier);
    }
    #endregion
}
