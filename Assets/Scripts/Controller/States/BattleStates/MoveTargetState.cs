using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTargetState : BattleState
{
    protected override void OnMove(object sender, object e)
    {
        Point p = (Point)e;
        SelectTile(p);
    }
}
