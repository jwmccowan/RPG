using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBattleState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("InitBattleState Enter");
        StartCoroutine(Init());
    }

    /// <summary>
    /// Inits the battle
    /// </summary>
    /// <returns></returns>
    IEnumerator Init()
    {
        board.Load(levelData);
        pos = new Point((int)levelData.tilePositions[0].x, (int)levelData.tilePositions[0].z);
        SelectTile(pos);
        yield return null;
        owner.ChangeState<MoveTargetState>();
    }
}
