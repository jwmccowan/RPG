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
        AbilityTargetState.baseAttack = false;
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
                menuOptions.Add("Liquid Healing");
                menuOptions.Add("Rock Throw");
                menuOptions.Add("Wind Blast");
                menuOptions.Add("Flame Strike");
                //And then lock based on the character's situation
                abilityMenuPanelController.SetLocked(1, true);
                abilityMenuPanelController.SetLocked(2, true);
                break;
            case ActionType.FullRoundAction:
                menuTitle = "Special Attacks";
                menuOptions.Add("Rocky Shell");
                menuOptions.Add("Ignite");
                menuOptions.Add("Typhoon");
                menuOptions.Add("Resurrect");
                break;
            default:
                Debug.LogError("Action Type not supported.");
                break;
        }
        abilityMenuPanelController.Show(menuTitle, menuOptions);
    }
}
