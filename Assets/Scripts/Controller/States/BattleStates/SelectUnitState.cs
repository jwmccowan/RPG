using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUnitState : BattleState
{
    int currentUnitIndex = -1;

    public override void Enter()
    {
        Debug.Log("SelectUnitState Enter");
        base.Enter();

        StartCoroutine("SelectUnit");
    }

    public override void Exit()
    {
        base.Exit();
        statPanelController.HidePrimary();
    }

    //TODO: select unit a bit more smart.  this is temp until we get stats up
    IEnumerator SelectUnit()
    {
        owner.round.MoveNext();
        SelectTile(turn.actor.tile.pos);
        RefreshPrimaryStatPanel(pos);
        yield return null;
        owner.ChangeState<CommandSelectionState>();
    }
}
