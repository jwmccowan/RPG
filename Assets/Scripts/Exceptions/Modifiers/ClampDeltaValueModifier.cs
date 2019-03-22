using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampDeltaValueModifier : ValueModifier
{
    public readonly int min;
    public readonly int max;

    public ClampDeltaValueModifier(int sortOrder, int min, int max)
        : base(sortOrder)
    {
        this.min = min;
        this.max = max;
    }

    public override int Modify(int fromValue, int toValue)
    {
        int delta = toValue - fromValue;
        return fromValue + Mathf.Clamp(delta, min, max);
    }
}