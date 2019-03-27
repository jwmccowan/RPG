using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBonusFeature : Feature
{
    #region fields
    public Bonus bonus;

    CharacterSheet characterSheet { get { return target.GetComponentInParent<CharacterSheet>(); } }
    #endregion

    #region protected
    protected override void OnApply()
    {
        characterSheet.AddBonus(bonus);
    }

    protected override void OnRemove()
    {
        characterSheet.RemoveBonus(bonus);
    }
    #endregion
}
