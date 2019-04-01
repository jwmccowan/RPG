using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreState : BattleState
{
    public override void Enter()
    {
        Debug.Log("ExploreState Enter");
        base.Enter();
        statPanelController.ShowPrimary(turn.actor.gameObject);
    }

    public override void Exit()
    {
        base.Exit();
        statPanelController.HidePrimary();
    }

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
