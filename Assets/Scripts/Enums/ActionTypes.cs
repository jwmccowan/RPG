using System;

[Flags]
public enum ActionType
{
    StandardAction = 0,
    MoveAction= 1 << 0,
    SwiftAction = 1 << 1,
    FullRoundAction = 1 << 2
}
