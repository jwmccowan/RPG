using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultValueModifier : ValueModifier
{
    public readonly int toMultiply;

    public MultValueModifier(int sortOrder, int toMultiply)
        : base(sortOrder)
    {
        this.toMultiply = toMultiply;
    }

    public override int Modify(int value)
    {
        return toMultiply * value;
    }
}
