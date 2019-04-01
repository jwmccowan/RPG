using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBonusFeature : Feature
{
    #region fields
    public Bonus bonus;

    BonusHandler bonusHandler { get { return target.GetComponentInParent<BonusHandler>(); } }
    #endregion

    #region protected
    protected override void OnApply()
    {
        bonusHandler.AddBonus(bonus);
    }

    protected override void OnRemove()
    {
        bonusHandler.RemoveBonus(bonus);
    }
    #endregion
}
