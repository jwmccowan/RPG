﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivideValueModifier : ValueModifier
{
    public readonly int toDivide;

    public DivideValueModifier(int sortOrder, int toDivide)
        : base(sortOrder)
    {
        this.toDivide = toDivide;
    }

    public override int Modify(int value)
    {
        if (value == 0)
        {
            Debug.LogError("Divide by zero error in DivideValueModifier.");
            return int.MaxValue;
        }
        return toDivide / value;
    }
}
