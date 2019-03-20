using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreState : BattleState
{
    protected override void OnFire(object sender, object e)
    {
        owner.ChangeState<CommandSelectionState>();
    }

    protected override void OnMove(object sender, object e)
    {
        Point p = (Point)e;
        SelectTile(pos + p);
    }
}
