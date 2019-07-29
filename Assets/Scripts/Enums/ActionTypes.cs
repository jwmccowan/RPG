using System;

[Flags]
public enum ActionType
{
    // This might change depending on how actions are used
    // Might just want to have a move and an ability
    StandardAction = 0,
    MoveAction= 1 << 0,
    SwiftAction = 1 << 1,
    FullRoundAction = 1 << 2
}
