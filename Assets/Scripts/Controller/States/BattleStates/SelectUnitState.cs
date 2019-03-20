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

    //TODO: select unit a bit more smart.  this is temp until we get stats up
    IEnumerator SelectUnit()
    {
        currentUnitIndex = (currentUnitIndex + 1) % units.Count;
        turn.Change(units[currentUnitIndex]);
        yield return null;
        owner.ChangeState<CommandSelectionState>();
    }
}
