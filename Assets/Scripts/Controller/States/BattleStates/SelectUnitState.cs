using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUnitState : BattleState
{
    public override void Enter()
    {
        Debug.Log("SelectUnitState Enter");
        base.Enter();
    }

    protected override void OnMove(object sender, object e)
    {
        Point p = (Point)e;
        SelectTile(pos + p);
    }

    protected override void OnFire(object sender, object e)
    {
        int i = (int)e;
        GameObject content = owner.currentTile.content;
        if (content != null)
        {
            owner.turn.actor = content.GetComponent<Unit>();
            owner.ChangeState<MoveTargetState>();
        }
    }
}
