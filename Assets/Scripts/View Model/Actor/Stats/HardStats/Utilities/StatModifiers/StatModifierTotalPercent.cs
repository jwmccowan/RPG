using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifierTotalPercent : StatModifier
{
    public override int order { get { return 30; } }

    public override float ApplyModifier(float statValue, float modifierValue)
    {
        return statValue * modifierValue;
    }

    public StatModifierTotalPercent(BonusTypes type) : base(type) { }
    public StatModifierTotalPercent(float value) : base(value) { }
    public StatModifierTotalPercent(BonusTypes type, float value) : base(type, value) { }
}
