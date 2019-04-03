using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformAbilityState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        turn.Act(ActionSelectionState.actionType);
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        yield return null;

        FakeAttack();

        //turn.actor.transform.Find("Jumper").GetComponent<Animator>().SetTrigger("attack");

        owner.ChangeState<CommandSelectionState>();
    }

    void FakeAttack()
    {
        for (int i = 0; i < turn.targets.Count; i++)
        {
            GameObject obj = turn.targets[i].content;
            CharacterSheet sheet = obj?.GetComponent<CharacterSheet>();
            if (sheet != null)
            {
                sheet.health.hp -= new Roll(1, 8, 2).value;
                if (sheet.health.hp <= 0)
                {
                    Debug.Log(string.Format("Knocked out {0}", obj));
                }
            }
        }
    }
}
