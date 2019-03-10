using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTargetState : BattleState
{
    List<Tile> tiles;

    public override void Enter()
    {
        Debug.Log("MoveTargetState Enter");
        base.Enter();
        Movement movement = owner.currentUnit.GetComponent<Movement>();
        tiles = movement.GetTilesInRange(board);
        board.SelectTiles(tiles);
    }

    public override void Exit()
    {
        base.Exit();
        board.DeselectTiles(tiles);
        tiles = null;
    }

    protected override void OnMove(object sender, object e)
    {
        Point p = (Point)e + pos;
        SelectTile(p);
    }

    protected override void OnFire(object sender, object e)
    {
        if (tiles.Contains(owner.currentTile))
        {
            owner.ChangeState<MoveSequenceState>();
        }
    }
}
