using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatModifier
{
    private float _value;
    private BonusTypes _type;
    private bool _applyToBase;
    public abstract int order { get; }
    public const string ValueDidChange = "StatModifier.ValueDidChange";

    public virtual bool applyToBase
    {
        get { return true; }
    }

    public BonusTypes bonusType
    {
        get { return _type; }
    }

    public float value
    {
        get { return _value; }
        set
        {
            if (!_value.Equals(value))
            {
                _value = value;
                this.PostNotification(ValueDidChange);
            }
        }
    }

    public StatModifier()
    {
        _type = BonusTypes.Untyped;
        _value = 0f;
    }

    public StatModifier(float value)
    {
        _type = BonusTypes.Untyped;
        _value = value;
    }

    public StatModifier(float value, BonusTypes type)
    {
        _type = type;
        _value = value;
    }

    public abstract float ApplyModifier(float statValue, float modifierValue);
}
