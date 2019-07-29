using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSelectionState : BaseAbilityMenuState
{
    public static ActionType actionType;

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
        owner.ChangeState<CategorySelectionState>();
    }

    protected override void Confirm()
    {
        turn.Act(actionType);
        owner.ChangeState<AbilityTargetState>();
    }

    protected override void LoadMenu()
    {
        if (menuOptions == null)
        {
            menuOptions = new List<string>();
        }
        menuOptions.Clear();
        //Here is where we query for possible actions and fill the list
        //This is all placeholder til we get there
        switch (actionType)
        {
            case ActionType.StandardAction:
                menuTitle = "Bending Abilities";
                menuOptions.Add("Mass Heal");
                menuOptions.Add("Direct Heal");
                menuOptions.Add("Poison Spores");
                menuOptions.Add("Growth");
                //And then lock based on the character's situation
                abilityMenuPanelController.SetLocked(1, true);
                abilityMenuPanelController.SetLocked(2, true);
                break;
            case ActionType.FullRoundAction:
                menuTitle = "Special Attacks";
                menuOptions.Add("Solar Beam");
                menuOptions.Add("Trample");
                break;
            default:
                Debug.LogError("Action Type not supported.");
                break;
        }
        abilityMenuPanelController.Show(menuTitle, menuOptions);
    }
}
