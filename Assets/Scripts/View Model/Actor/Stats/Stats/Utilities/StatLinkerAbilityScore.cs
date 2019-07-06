using UnityEngine;

public class StatLinkerAbilityScore : StatLinker
{
    private float ratio;
    private StatAbilityScore abilityScore;

    public override float value
    {
        get {
            return abilityScore.statAbilityScoreBonus * ratio;
        }
    }

    public StatLinkerAbilityScore(StatAbilityScore abilityScore, float ratio)
        : base(abilityScore)
    {
        this.ratio = ratio;
        this.abilityScore = abilityScore;
    }
}
