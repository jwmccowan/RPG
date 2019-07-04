using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifierBaseAdd : StatModifier
{
    public override int order { get { return 20; } }

    public override float ApplyModifier(float statValue, float modifierValue)
    {
        return modifierValue;
    }

    public StatModifierBaseAdd(BonusTypes type) : base(type) { }
    public StatModifierBaseAdd(float value) : base(value) { }
    public StatModifierBaseAdd(BonusTypes type, float value) : base(type, value) { }
}
