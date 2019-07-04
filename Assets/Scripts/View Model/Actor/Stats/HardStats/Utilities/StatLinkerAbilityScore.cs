using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatLinkerAbilityScore : StatLinker
{
    private float ratio;

    public override float value
    {
        get {
            float abilityScoreBonus = Mathf.Floor(stat.statValue - 10);
            return abilityScoreBonus * ratio;
        }
    }

    public StatLinkerAbilityScore(Stat stat, float ratio)
        : base(stat)
    {
        this.ratio = ratio;
    }
}
