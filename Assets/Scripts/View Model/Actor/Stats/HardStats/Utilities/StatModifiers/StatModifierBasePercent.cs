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

    public StatModifierBasePercent(BonusTypes type) : base(type) { }
    public StatModifierBasePercent(float value) : base(value) { }
    public StatModifierBasePercent(BonusTypes type, float value) : base(type, value) { }
}
