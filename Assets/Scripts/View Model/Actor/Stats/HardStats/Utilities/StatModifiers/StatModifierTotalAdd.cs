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

    public StatModifierTotalAdd(BonusTypes type) : base(type) { }
    public StatModifierTotalAdd(float value) : base(value) { }
    public StatModifierTotalAdd(BonusTypes type, float value) : base(type, value) { }
}
