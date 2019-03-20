using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseException
{
    public bool toggle { get; private set; }
    bool defaultToggle;

    public BaseException(bool defaultToggle)
    {
        this.defaultToggle = defaultToggle;
    }

    public void FlipToggle()
    {
        toggle = !defaultToggle;
    }
}
