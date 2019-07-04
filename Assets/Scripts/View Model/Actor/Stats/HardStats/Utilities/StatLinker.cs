using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatLinker
{
    private Stat _stat;

    public Stat stat
    {
        get { return _stat; }
    }

    public abstract float value { get; }

    public StatLinker(Stat stat)
    {
        this._stat = stat;
    }
}
