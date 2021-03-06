﻿public class Stat
{
    private string _statName;
    private float _statValue;
    private float _statBaseValue;
    private StatTypes _statType;
    private StatCollection _owner;

    /// <summary>
    /// The name of the stat.
    /// This is a property to allow more specific stats to overwrite.
    /// </summary>
    public string statName
    {
        get { return _statName; }
        set { _statName = value; }
    }

    /// <summary>
    /// The value of the stat.
    /// This is a property to allow more specific stats to overwrite.
    /// </summary>
    public virtual float statBaseValue
    {
        get { return _statBaseValue; }
        set {
            if (_statBaseValue != value)
            {
                float oldValue = statValue;
                _statBaseValue = value;
                //TODO: I don't think this is right...
                owner.PostNotification(StatCollection.ValueDidChange(statType), new StatValueChangeArgs(statType, oldValue, statValue));
            }
        }
    }

    /// <summary>
    /// The StatCollection which owns this stat.
    /// </summary>
    public StatCollection owner
    {
        get { return _owner; }
        set { _owner = value; }
    }

    /// <summary>
    /// The enum value of the stat.
    /// </summary>
    public virtual StatTypes statType
    {
        get { return _statType; }
        set { _statType = value; }
    }

    /// <summary>
    /// The value of the stat.
    /// This is a property to allow more specific stats to overwrite.
    /// </summary>
    public virtual float statValue
    {
        get { return _statBaseValue; }
    }

    /// <summary>
    /// A generic stat initialized to an empty name and a value of 0.
    /// </summary>
    public Stat()
    {
        statName = string.Empty;
        statBaseValue = 0.0f;
    }

    /// <summary>
    /// A simple stat, with only a name and a value
    /// </summary>
    /// <param name="statName" type="string">The readable name of the stat.</param>
    /// <param name="statValue" type="float">The initial value of the stat.</param>
    public Stat(string name, float value)
    {
        this.statName = name;
        this.statBaseValue = value;
    }
}
