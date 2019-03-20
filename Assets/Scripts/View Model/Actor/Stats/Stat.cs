using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    public virtual int bonus
    {
        get
        {
            return value;
        }
    }

    public virtual int value
    {
        get
        {
            return value;
        }
        set
        {
            this.value = value;
        }
    }
    
    public virtual int baseValue
    {
        get
        {
            return baseValue;
        }
        set
        {
            baseValue = value;
        }
    }

    public Stat(int value)
    {
        baseValue = value;
        Reset();
    }

    // This might be too much power in one hand.
    // Not really any benefit to it being public, so marking it private.
    protected virtual void Reset()
    {
        value = baseValue;
    }
}
