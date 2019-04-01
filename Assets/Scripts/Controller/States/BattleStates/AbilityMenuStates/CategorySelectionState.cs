using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategorySelectionState : BaseAbilityMenuState
{
    //The following is all temporarily hard coded

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
        owner.ChangeState<CommandSelectionState>();
    }

    protected override void Confirm()
    {
        switch (abilityMenuPanelController.selection)
        {
            case 0:
                ActionSelectionState.actionType = ActionType.MoveAction;
                owner.ChangeState<ActionSelectionState>();
                break;
            case 1:
                ActionSelectionState.actionType = ActionType.StandardAction;
                owner.ChangeState<ActionSelectionState>();
                break;
            case 2:
                ActionSelectionState.actionType = ActionType.FullRoundAction;
                owner.ChangeState<ActionSelectionState>();
                break;
            case 3:
                ActionSelectionState.actionType = ActionType.SwiftAction;
                owner.ChangeState<ActionSelectionState>();
                break;
        }
    }

    protected override void LoadMenu()
    {
        if (menuOptions == null)
        {
            menuOptions = new List<string>(4);
            menuTitle = "Action Type";
            menuOptions.Add("Move Action");
            menuOptions.Add("Standard Action");
            menuOptions.Add("Full Round Action");
            menuOptions.Add("Swift Action");
        }
        abilityMenuPanelController.Show(menuTitle, menuOptions);
        // hardcoding reason for locking actions as true
        // in the future, we'll query whether they can or not
        // using turn.usedMoveAction etc
        abilityMenuPanelController.SetLocked(0, turn.usedMoveAction); // especially 
        abilityMenuPanelController.SetLocked(1, turn.usedStandardAction);
        abilityMenuPanelController.SetLocked(2, turn.usedMoveAction || turn.usedStandardAction);
        abilityMenuPanelController.SetLocked(3, turn.usedSwiftAction);
    }
}
