public class HealthModifierFeature : Feature
{
    #region fields
    public int value;

    CharacterSheet sheet { get { return target.GetComponentInParent<CharacterSheet>(); } }
    #endregion

    #region protected
    protected override void OnApply()
    {
        sheet.hp += value;
    }

    protected override void OnRemove()
    {
        sheet.hp += value;
    }
    #endregion
}
