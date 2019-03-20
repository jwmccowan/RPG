using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandSelectionState : BaseAbilityMenuState
{
    public override void Enter()
    {
        Debug.Log("CommandSelectionState Enter");
        base.Enter();
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
                owner.ChangeState<MoveTargetState>();
                break;
            case 1:
                owner.ChangeState<CategorySelectionState>();
                break;
            case 2:
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
            menuOptions.Add("Move");
            menuOptions.Add("Action");
            menuOptions.Add("Hold");
        }

        abilityMenuPanelController.Show(menuTitle, menuOptions);
        abilityMenuPanelController.SetLocked(0, turn.usedMoveAction);
        abilityMenuPanelController.SetLocked(1, turn.usedMoveAction && turn.usedStandardAction && turn.usedSwiftAction);
    }
}
