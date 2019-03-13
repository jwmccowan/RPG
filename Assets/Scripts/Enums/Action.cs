using System;

[Flags]
public enum Action
{
    StandardAction = 0,
    MoveAction= 1 << 0,
    SwiftAction = 1 << 1,
    FullRoundAction = 1 << 2
}
