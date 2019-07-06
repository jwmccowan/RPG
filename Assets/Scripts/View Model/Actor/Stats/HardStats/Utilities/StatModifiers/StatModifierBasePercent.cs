using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifierBasePercent : StatModifier
{
    public override int order { get { return 10; } }

    public override float ApplyModifier(float statValue, float modifierValue)
    {
        return statValue * modifierValue;
    }

    public StatModifierBasePercent(float value) : base(value) { }
    public StatModifierBasePercent(float value, BonusTypes type) : base(value, type) { }
}
