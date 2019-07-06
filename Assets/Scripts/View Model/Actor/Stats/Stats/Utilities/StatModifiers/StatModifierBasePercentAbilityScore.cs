public class StatModifierBasePercentAbilityScore : StatModifier
{
    private float abilityScoreBonus;

    public override int order { get { return 9; } }

    public override float ApplyModifier(float statValue, float modifierValue)
    {
        return statValue * modifierValue * abilityScoreBonus;
    }

    public StatModifierBasePercentAbilityScore(float value, StatAbilityScore abilityScore) : base(value)
    {
        abilityScoreBonus = abilityScore.statAbilityScoreBonus;
        this.AddListener(OnAbilityScoreChanged, StatCollection.ValueDidChange(abilityScore.statType));
    }

    public StatModifierBasePercentAbilityScore(float value, BonusTypes type, StatAbilityScore abilityScore) : base(value, type)
    {
        abilityScoreBonus = abilityScore.statAbilityScoreBonus;
        this.AddListener(OnAbilityScoreChanged, StatCollection.ValueDidChange(abilityScore.statType));
    }

    private void OnAbilityScoreChanged(object sender, object e)
    {
        StatAbilityScore abilityScore = e as StatAbilityScore;
        abilityScoreBonus = abilityScore.statAbilityScoreBonus;
        this.PostNotification(ValueDidChange);
    }
}
