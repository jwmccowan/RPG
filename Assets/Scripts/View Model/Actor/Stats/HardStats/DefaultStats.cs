using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultStats : StatCollection
{
    protected override void ConfigureStats()
    {
        base.ConfigureStats();

        StatAttribute might = GetOrCreateStat<StatAttribute>(StatTypes.Ability_Score_Might);
        might.statName = "Might";
        might.statBaseValue = 10f;

        StatAttribute perception = GetOrCreateStat<StatAttribute>(StatTypes.Ability_Score_Perception);
        perception.statName = "Perception";
        perception.statBaseValue = 10f;

        StatAttribute constitution = GetOrCreateStat<StatAttribute>(StatTypes.Ability_Score_Constitution);
        constitution.statName = "Constitution";
        constitution.statBaseValue = 10f;

        StatAttribute mobility = GetOrCreateStat<StatAttribute>(StatTypes.Ability_Score_Mobility);
        mobility.statName = "Mobility";
        mobility.statBaseValue = 10f;

        StatAttribute health = GetOrCreateStat<StatAttribute>(StatTypes.Stat_Max_HP);
        health.statName = "Health";
        health.statBaseValue = 20f;
        health.AddAbilityScorePercentModifier(constitution, .05f);

        StatAttribute initiative = GetOrCreateStat<StatAttribute>(StatTypes.Stat_Initiative);
        initiative.statName = "Initiative";
        initiative.statBaseValue = 5f;
        initiative.AddLinker(new StatLinkerAbilityScore(mobility, 1f));

        StatAttribute accuracy = GetOrCreateStat<StatAttribute>(StatTypes.Stat_Accuracy);
        accuracy.statName = "Accuracy";
        accuracy.statBaseValue = 20f;
        accuracy.AddLinker(new StatLinkerAbilityScore(perception, 1f));

        StatAttribute damageModifier = GetOrCreateStat<StatAttribute>(StatTypes.Stat_Damage_Modifier);
        damageModifier.statName = "DamageModifier";
        damageModifier.statBaseValue = 1f;
        damageModifier.AddAbilityScorePercentModifier(might, .03f);

        StatAttribute movement = GetOrCreateStat<StatAttribute>(StatTypes.Stat_Movement);
        movement.statName = "Movement";
        movement.statBaseValue = 3f;
        movement.AddLinker(new StatLinkerAbilityScore(mobility, .25f));

        /*
        Level,
    Experience,
    Ability_Score_Might,
    Ability_Score_Constitution,
    Ability_Score_Mobility,
    Ability_Score_Perception,
    Stat_Damage_Modifier,
    Stat_Max_HP,
    Stat_Accuracy,
    Stat_Initiative,
    Stat_Deflection,
    Stat_Resolve,
    Stat_Movement,
    */
    }
}
