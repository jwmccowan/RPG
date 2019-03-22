using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddValueModifier : ValueModifier
{
    public readonly int toAdd;

    public AddValueModifier(int sortOrder, int toAdd)
        : base(sortOrder)
    {
        this.toAdd = toAdd;
    }

    public override int Modify(int fromValue, int toValue)
    {
        return toAdd + toValue;
    }
}
