using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatLinkerBasic : StatLinker
{
    private float ratio;

    public override float value
    {
        get { return stat.statValue * ratio; }
    }

    public StatLinkerBasic(Stat stat, float ratio)
        : base(stat)
    {
        this.ratio = ratio;
    }
}
