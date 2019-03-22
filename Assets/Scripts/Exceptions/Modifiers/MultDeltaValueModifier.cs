using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultDeltaValueModifier : ValueModifier
{
    public readonly int toMultiply;

    public MultDeltaValueModifier(int sortOrder, int toMultiply)
        : base(sortOrder)
    {
        this.toMultiply = toMultiply;
    }

    public override int Modify(int fromValue, int toValue)
    {
        int delta = toValue - fromValue;
        return fromValue + toMultiply * delta;
    }
}
