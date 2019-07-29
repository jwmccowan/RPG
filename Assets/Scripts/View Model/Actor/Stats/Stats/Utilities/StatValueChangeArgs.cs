using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatValueChangeArgs
{
    private StatTypes _statType;
    private float _oldValue;
    private float _newValue;

    public StatTypes statType { get { return _statType; } }
    public float oldValue { get { return _oldValue; } }
    public float newValue { get { return _newValue; } }

    public StatValueChangeArgs(StatTypes statType, float oldValue, float newValue)
    {
        this._statType = statType;
        this._oldValue = oldValue;
        this._newValue = newValue;
    }
}
