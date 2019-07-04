using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatModifier
{
    private float _value;
    private BonusTypes _type;
    public abstract int order { get; }
    public const string ValueDidChange = "StatModifier.ValueDidChange";

    public BonusTypes bonusType
    {
        get { return _type; }
    }

    public float value
    {
        get { return _value; }
        set {
            if (value != _value)
            {
                _value = value;
                this.PostNotification(ValueDidChange);
            }
        }
    }

    public StatModifier()
    {
        _type = BonusTypes.None;
        _value = 0f;
    }

    public StatModifier(BonusTypes type)
    {
        _type = type;
        _value = 0f;
    }

    public StatModifier(BonusTypes type, float value)
    {
        _type = type;
        _value = value;
    }
}
