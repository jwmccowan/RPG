using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityScore : Stat
{
    public override int bonus
    {
        get
        {
            return (value - 10) / 2;
        }
    }

    public AbilityScore(int value) : base(value) { }
}
