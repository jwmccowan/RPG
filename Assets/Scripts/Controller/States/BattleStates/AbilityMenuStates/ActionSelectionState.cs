using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSelectionState : BaseAbilityMenuState
{
    public static ActionType actionType;
    protected override void Cancel()
    {
        owner.ChangeState<CategorySelectionState>();
    }

    protected override void Confirm()
    {
        turn.Act(actionType);
        owner.ChangeState<CommandSelectionState>();
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
                menuTitle = "Standard";
                menuOptions.Add("Attack");
                menuOptions.Add("Magic Missile");
                menuOptions.Add("Bless");
                menuOptions.Add("Shield Bash");
                //And then lock based on the character's situation
                abilityMenuPanelController.SetLocked(1, true);
                abilityMenuPanelController.SetLocked(2, true);
                break;
            case ActionType.MoveAction:
                menuTitle = "Move";
                menuOptions.Add("Draw Weapon");
                menuOptions.Add("Open Door");
                menuOptions.Add("Move");
                break;
            case ActionType.FullRoundAction:
                menuTitle = "Full Round";
                menuOptions.Add("Full Attack");
                menuOptions.Add("Summon Monster");
                menuOptions.Add("Run");
                break;
            case ActionType.SwiftAction:
                menuTitle = "Swift";
                //Gettig lazy but this is all placeholder anyways
                menuOptions.Add("Swift Action");
                menuOptions.Add("Swift Action");
                menuOptions.Add("Swift Action");
                break;
            default:
                Debug.LogError("Action Type not supported.");
                break;
        }
        abilityMenuPanelController.Show(menuTitle, menuOptions);
    }
}
