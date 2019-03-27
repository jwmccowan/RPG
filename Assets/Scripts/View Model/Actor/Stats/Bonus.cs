using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus
{
    public StatTypes statType;
    public BonusTypes bonusType;
    public int value;

    bool isActivated;

    public Bonus(StatTypes statType, BonusTypes bonusType, int value)
    {
        this.statType = statType;
        this.bonusType = bonusType;
        this.value = value;
        isActivated = false;
    }

    //public void Activate()
    //{
    //    if (!isActivated)
    //    {
    //        isActivated = true;
    //        this.AddListener(OnStatChanged, Stats.WillChangeNotification(statType), stats);
    //    }
    //}

    //public void Deactivate()
    //{
    //    if (isActivated)
    //    {
    //        isActivated = false;
    //        this.RemoveListener(OnStatChanged, Stats.WillChangeNotification(statType), stats);
    //    }
    //}

    //void OnStatChanged(object sender, object e)
    //{
    //    ValueChangeException vce = e as ValueChangeException;
    //    vce.AddModifier(new AddValueModifier(0, value));
    //}
}
