using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DerivedStat : Stat
{
    List<Stat> baseStats;

    public override int bonus
    {
        get
        {
            int retValue = value;
            foreach(Stat s in baseStats)
            {
                retValue += s.bonus;
            }
            return retValue;
        }
    }

    public DerivedStat(int value, List<Stat> baseStats)
        : base(value)
    {
        this.baseStats = new List<Stat>();
        for (int i = 0; i < baseStats.Count; i++)
        {
            this.baseStats.Add(baseStats[i]);
        }
    }
}
