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
                ActionSelectionState.actionType = ActionType.StandardAction;
                owner.ChangeState<ActionSelectionState>();
                break;
            case 1:
                ActionSelectionState.actionType = ActionType.FullRoundAction;
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
            menuOptions.Add("Bending Abilities");
            menuOptions.Add("Special Attacks");
        }
        abilityMenuPanelController.Show(menuTitle, menuOptions);

        //TODO: Hide based on whatever reason
        abilityMenuPanelController.SetLocked(0, turn.usedStandardAction);
        abilityMenuPanelController.SetLocked(1, turn.usedMoveAction);
        abilityMenuPanelController.SetLocked(1, turn.usedStandardAction || turn.usedMoveAction);
    }
}
