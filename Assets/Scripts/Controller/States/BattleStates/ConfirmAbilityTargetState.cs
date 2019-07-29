using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmAbilityTargetState : BattleState
{
    List<Tile> tiles;
    AbilityArea aa;
    int index = 0;

    public override void Enter()
    {
        base.Enter();
        aa = turn.ability.GetComponent<AbilityArea>();
        tiles = aa.GetTilesInArea(board, pos);
        board.SelectTiles(tiles);
        FindTargets();
        RefreshPrimaryStatPanel(turn.actor.tile.pos);
        SetTarget(0);
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
        Point p = (Point)e;
        if (p.y > 0 || p.x > 0)
        {
            SetTarget(index + 1);
        }
        else
        {
            SetTarget(index - 1);
        }
    }

    protected override void OnFire(object sender, object e)
    {
        int i = (int)e;
        if (i == 0)
        {
            if (turn.targets.Count > 0)
            {
                owner.ChangeState<PerformAbilityState>();
            }
        }
        else
        {
            owner.ChangeState<AbilityTargetState>();
        }
    }

    void FindTargets()
    {
        turn.targets = new List<Tile>();
        AbilityEffectTarget[] targetList = turn.ability.GetComponentsInChildren<AbilityEffectTarget>();
        for (int i = 0; i < tiles.Count; i++)
        {
            if (IsTarget(tiles[i], targetList))
            {
                turn.targets.Add(tiles[i]);
            }
        }
    }

    bool IsTarget(Tile tile, AbilityEffectTarget[] targets)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].IsTarget(tile))
            {
                return true;
            }
        }
        return false;
    }

    void SetTarget(int i)
    {
        index = i;
        if (index < 0)
        {
            index = turn.targets.Count - 1;
        }
        if (index >= turn.targets.Count)
        {
            index = 0;
        }
        if (turn.targets.Count > 0)
        {
            RefreshSecondaryStatPanel(turn.targets[index].pos);
        }
    }
}
