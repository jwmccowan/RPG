using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSequenceState : BattleState
{
    public override void Enter()
    {
        Debug.Log("InitBattleState Enter");
        base.Enter();
        StartCoroutine("Sequence");
    }

    IEnumerator Sequence()
    {
        Movement m = owner.turn.actor.GetComponent<Movement>();
        yield return m.Traverse(owner.currentTile);
        turn.Act(ActionType.MoveAction);
        owner.ChangeState<CommandSelectionState>();
    }
}
