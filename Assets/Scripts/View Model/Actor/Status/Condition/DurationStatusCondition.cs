using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurationStatusCondition : StatusCondition
{
    public int duration = 10;
    Unit unit;

    private void OnEnable()
    {
        unit = GetComponentInParent<Unit>();
        this.AddListener(OnTurnBegan, InitiativeController.TurnBeganNotification, unit);
    }

    private void OnDisable()
    {
        this.RemoveListener(OnTurnBegan, InitiativeController.TurnBeganNotification, unit);
    }

    void OnTurnBegan(object sender, object e)
    {
        duration--;
        if (duration <= 0)
        {
            Remove();
        }
    }
}
