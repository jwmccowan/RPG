using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifierFeature : Feature
{
    #region fields
    public StatTypes statType;
    public int value;

    Stats stats { get { return target.GetComponentInParent<Stats>(); } }
    #endregion

    #region protected
    protected override void OnApply()
    {
        stats[statType] += value;
    }

    protected override void OnRemove()
    {
        stats[statType] -= value;
    }
    #endregion
}
