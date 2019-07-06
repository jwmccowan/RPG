using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultStats : StatCollection
{
    protected override void ConfigureStats()
    {
        base.ConfigureStats();

        StatAbilityScore might = GetOrCreateStat<StatAbilityScore>(StatTypes.Ability_Score_Might);
        might.statName = "Might";
        might.statBaseValue = 10f;

        StatAbilityScore perception = GetOrCreateStat<StatAbilityScore>(StatTypes.Ability_Score_Perception);
        perception.statName = "Perception";
        perception.statBaseValue = 10f;

        StatAbilityScore constitution = GetOrCreateStat<StatAbilityScore>(StatTypes.Ability_Score_Constitution);
        constitution.statName = "Constitution";
        constitution.statBaseValue = 10f;

        StatAbilityScore mobility = GetOrCreateStat<StatAbilityScore>(StatTypes.Ability_Score_Mobility);
        mobility.statName = "Mobility";
        mobility.statBaseValue = 10f;

        StatRange health = GetOrCreateStat<StatRange>(StatTypes.Stat_Max_HP);
        health.statName = "Health";
        health.statBaseValue = 20f;
        health.AddModifier(new StatModifierBasePercentAbilityScore(.05f, constitution));

        StatAttribute initiative = GetOrCreateStat<StatAttribute>(StatTypes.Stat_Initiative);
        initiative.statName = "Initiative";
        initiative.statBaseValue = 5f;
        initiative.AddLinker(new StatLinkerAbilityScore(mobility, 1f));

        StatAttribute accuracy = GetOrCreateStat<StatAttribute>(StatTypes.Stat_Accuracy);
        accuracy.statName = "Accuracy";
        accuracy.statBaseValue = 20f;
        accuracy.AddLinker(new StatLinkerAbilityScore(perception, 1f));

        StatAttribute deflection = GetOrCreateStat<StatAttribute>(StatTypes.Stat_Deflection);
        deflection.statName = "Deflection";
        deflection.statBaseValue = 20f;
        deflection.AddLinker(new StatLinkerAbilityScore(mobility, 2f));

        StatAttribute resolve = GetOrCreateStat<StatAttribute>(StatTypes.Stat_Resolve);
        resolve.statName = "Resolve";
        resolve.statBaseValue = 20f;
        resolve.AddLinker(new StatLinkerAbilityScore(might, 1f));
        resolve.AddLinker(new StatLinkerAbilityScore(constitution, 1f));

        StatAttribute damageModifier = GetOrCreateStat<StatAttribute>(StatTypes.Stat_Damage_Modifier);
        damageModifier.statName = "DamageModifier";
        damageModifier.statBaseValue = 1f;
        damageModifier.AddModifier(new StatModifierBasePercentAbilityScore(.03f, might));

        StatAttribute movement = GetOrCreateStat<StatAttribute>(StatTypes.Stat_Movement);
        movement.statName = "Movement";
        movement.statBaseValue = 3f;
        movement.AddLinker(new StatLinkerAbilityScore(mobility, .25f));
    }
}
