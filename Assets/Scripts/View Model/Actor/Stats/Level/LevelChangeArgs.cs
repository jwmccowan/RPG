using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChangeArgs
{
    private int _oldLevel;
    private int _newLevel;

    public int oldLevel
    {
        get { return _oldLevel; }
    }

    public int newLevel
    {
        get { return _newLevel; }
    }

    public LevelChangeArgs(int oldLevel, int newLevel)
    {
        _oldLevel = oldLevel;
        _newLevel = newLevel;
    }
}
