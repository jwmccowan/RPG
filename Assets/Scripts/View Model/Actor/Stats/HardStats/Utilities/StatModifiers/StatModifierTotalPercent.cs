public class StatModifierTotalPercent : StatModifier
{
    public override int order { get { return 30; } }

    public override float ApplyModifier(float statValue, float modifierValue)
    {
        return statValue * modifierValue;
    }

    public override bool applyToBase
    {
        get { return false; }
    }

    public StatModifierTotalPercent(float value) : base(value) { }
    public StatModifierTotalPercent(float value, BonusTypes type) : base(value, type) { }
}
