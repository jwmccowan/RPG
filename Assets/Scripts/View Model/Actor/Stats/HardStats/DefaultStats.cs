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
        might.statBaseValue = 14f;

        StatAttribute constitution = GetOrCreateStat<StatAttribute>(StatTypes.Ability_Score_Constitution);
        constitution.statName = "Constitution";
        constitution.statBaseValue = 16f;

        StatAttribute mobility = GetOrCreateStat<StatAttribute>(StatTypes.Ability_Score_Mobility);
        mobility.statName = "Mobility";
        mobility.statBaseValue = 16f;

        StatAttribute health = GetOrCreateStat<StatAttribute>(StatTypes.Stat_Max_HP);
        health.statName = "Health";
        health.statBaseValue = 80f;
        health.AddLinker(new StatLinkerBasic(constitution, 2f));

        StatAttribute initiative = GetOrCreateStat<StatAttribute>(StatTypes.Stat_Initiative);
        initiative.statName = "Initiative";
        initiative.statBaseValue = 5f;
        initiative.AddLinker(new StatLinkerBasic(mobility, 1f));
    }
}
