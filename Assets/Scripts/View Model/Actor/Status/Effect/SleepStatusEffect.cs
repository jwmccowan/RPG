using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepStatusEffect : StatusEffect
{
    Unit unit;

    void OnEnable()
    {
        unit = GetComponentInParent<Unit>();
        this.AddListener(OnTurnBegan, InitiativeController.TurnBeganNotification, unit);
        this.AddListener(OnStatusCheck, AbilityAccuracy.StatusCheckNotification, unit);
    }

    void OnDisable()
    {
        this.RemoveListener(OnTurnBegan, InitiativeController.TurnBeganNotification, unit);
    }

    void OnTurnBegan(object sender, object e)
    {
        BaseException exc = e as BaseException;
        exc.FlipToggle();
        Debug.Log(string.Format("{0} is sleeping and skipped his turn.", unit.name));
    }

    void OnStatusCheck(object sender, object e)
    {
        Info<Unit, Unit, int> info = e as Info<Unit, Unit, int>;

    }
}
