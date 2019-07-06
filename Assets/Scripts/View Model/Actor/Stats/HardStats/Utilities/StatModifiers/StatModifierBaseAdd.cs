public class StatModifierBaseAdd : StatModifier
{
    public override int order { get { return 20; } }

    public override float ApplyModifier(float statValue, float modifierValue)
    {
        return modifierValue;
    }

    public StatModifierBaseAdd(float value) : base(value) { }
    public StatModifierBaseAdd(float value, BonusTypes type) : base(value, type) { }
}
