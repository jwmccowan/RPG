using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTargetState : BattleState
{
    List<Tile> tiles;
    AbilityRange ar;

    public override void Enter()
    {
        Debug.Log("AbilityTargetState Enter");
        base.Enter();
        ar = turn.ability.GetComponent<AbilityRange>();
        SelectTiles();
        statPanelController.ShowPrimary(turn.actor.gameObject);
        if (ar.directionOriented) RefreshSecondaryStatPanel(pos);
    }

    public override void Exit()
    {
        base.Exit();
        board.DeselectTiles(tiles);
        statPanelController.HidePrimary();
        statPanelController.HideSecondary();
    }

    protected override void OnMove(object sender, object e)
    {
        if (ar.directionOriented)
        {
            ChangeDirection((Point)e);
        }
        else
        {
            SelectTile((Point)e + pos);
            RefreshSecondaryStatPanel(pos);
        }
    }

    protected override void OnFire(object sender, object e)
    {
        if ((int)e == 0)
        {
            if (ar.directionOriented || tiles.Contains(board.GetTile(pos)))
            {
                owner.ChangeState<ConfirmAbilityTargetState>();
            }
        }
        else
        {
            owner.ChangeState<CategorySelectionState>();
        }
    }

    void ChangeDirection(Point p)
    {
        Directions dir = p.GetDirection();
        if (turn.actor.dir != dir)
        {
            board.DeselectTiles(tiles);
            turn.actor.dir = dir;
            turn.actor.Match();
            SelectTiles();
        }
    }

    void SelectTiles()
    {
        tiles = ar.GetTilesInRange(board);
        board.SelectTiles(tiles);
    }
}
