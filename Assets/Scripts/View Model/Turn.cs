﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn
{
    public bool usedMoveAction { get; private set; }
    public bool usedStandardAction { get; private set; }
    public bool usedSwiftAction { get; private set; }
    public Unit actor;
    public Tile startTile;
    public Directions startDir;

    public void Act(ActionType actionType)
    {
        usedMoveAction = (actionType == (ActionType.FullRoundAction | ActionType.MoveAction));
        usedStandardAction = (actionType == (ActionType.FullRoundAction | ActionType.StandardAction));
        usedSwiftAction = (actionType == (ActionType.SwiftAction));
    }

    public void Change(Unit newActor)
    {
        actor = newActor;
        usedMoveAction = false;
        usedStandardAction = false;
        usedSwiftAction = false;
        startTile = actor.tile;
        startDir = actor.dir;
    }
}
