using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbilityMenuState : BattleState
{
    protected string menuTitle;
    protected List<string> menuOptions;

    public override void Enter()
    {
        base.Enter();
        SelectTile(turn.actor.tile.pos);
        LoadMenu();
    }

    public override void Exit()
    {
        base.Exit();
        abilityMenuPanelController.Hide();
    }

    protected override void OnFire(object sender, object e)
    {
        int i = (int)e;
        if (i == 0)
        {
            Confirm();
        }
        else if (i == 1)
        {
            Cancel();
        }
    }

    protected override void OnMove(object sender, object e)
    {
        Point p = (Point)e;
        if (p.x < 0 || p.y < 0)
        {
            abilityMenuPanelController.Next();
        }
        else
        {
            abilityMenuPanelController.Previous();
        }
    }

    protected abstract void LoadMenu();
    protected abstract void Confirm();
    protected abstract void Cancel();
}
