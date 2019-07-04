using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifier
{
    public enum Types
    {
        None, 
        BaseValuePercent,
        BaseValueAdd,
        TotalValuePercent,
        TotalValueAdd
    }

    private Types _type;
    private float _value;
    private StatTypes _statType;

    public Types type
    {
        get { return _type; }
        set { _type = value; }
    }

    public float value
    {
        get { return _value; }
        set { _value = value; }
    }

    public StatTypes statType
    {
        get { return _statType; }
        set { _statType = value; }
    }

    public StatModifier()
    {
        _type = Types.None;
        _value = 0f;
        _statType = StatTypes.Null;
    }

    public StatModifier(Types type, StatTypes statType, float value)
    {
        _type = type;
        _value = value;
        _statType = statType;
    }
}
