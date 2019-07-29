using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandSelectionState : BaseAbilityMenuState
{
    public override void Enter()
    {
        Debug.Log("CommandSelectionState Enter");
        base.Enter();
        statPanelController.ShowPrimary(turn.actor.gameObject);
    }

    public override void Exit()
    {
        base.Exit();
        statPanelController.HidePrimary();
    }

    protected override void Cancel()
    {
        owner.ChangeState<ExploreState>();
    }

    protected override void Confirm()
    {
        switch (abilityMenuPanelController.selection)
        {
            case 0:
                turn.ability = turn.actor.GetComponentInChildren<AbilityRange>().gameObject;
                owner.ChangeState<AbilityTargetState>();
                break;
            case 1:
                owner.ChangeState<MoveTargetState>();
                break;
            case 2:
                owner.ChangeState<CategorySelectionState>();
                break;
            case 3:
                owner.ChangeState<SelectUnitState>();
                break;
        }
    }

    protected override void LoadMenu()
    {
        if (menuOptions == null)
        {
            menuTitle = "Command";
            menuOptions = new List<string>(3);
            menuOptions.Add("Attack");
            menuOptions.Add("Move");
            menuOptions.Add("Ability");
            menuOptions.Add("Hold");
        }

        abilityMenuPanelController.Show(menuTitle, menuOptions);
        abilityMenuPanelController.SetLocked(0, turn.usedStandardAction);
        abilityMenuPanelController.SetLocked(1, turn.usedMoveAction);
        abilityMenuPanelController.SetLocked(2, turn.usedMoveAction);
    }
}
