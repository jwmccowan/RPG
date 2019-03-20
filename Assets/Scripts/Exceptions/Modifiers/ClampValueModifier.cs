using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampValueModifier : ValueModifier
{
    public readonly int min;
    public readonly int max;

    public ClampValueModifier(int sortOrder, int min, int max)
        : base(sortOrder)
    {
        this.min = min;
        this.max = max;
    }

    public override int Modify(int value)
    {
        return Mathf.Clamp(value, min, max);
    }
}
