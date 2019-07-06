using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifierTotalAdd : StatModifier
{
    public override int order { get { return 40; } }

    public override float ApplyModifier(float statValue, float modifierValue)
    {
        return modifierValue;
    }

    public override bool applyToBase
    {
        get { return false; }
    }

    public StatModifierTotalAdd(float value) : base(value) { }
    public StatModifierTotalAdd(float value, BonusTypes type) : base(value, type) { }
}
