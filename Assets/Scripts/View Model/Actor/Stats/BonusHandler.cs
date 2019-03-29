using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusHandler : MonoBehaviour
{
    Stats stats;
    Dictionary<StatTypes, Dictionary<BonusTypes, List<Bonus>>> bonusMap;

    void Awake()
    {
        stats = GetComponent<Stats>();
        bonusMap = new Dictionary<StatTypes, Dictionary<BonusTypes, List<Bonus>>>();
    }

    public void AddBonus(Bonus bonus)
    {
        ValidateBonusPath(bonus.statType, bonus.bonusType);
        Bonus max = GetHighestBonus(bonus.statType, bonus.bonusType);
        bonusMap[bonus.statType][bonus.bonusType].Add(bonus);
        int delta = bonus.value - max.value;
        if (delta > 0)
        {
            stats[bonus.statType] += delta;
        }
    }

    public void RemoveBonus(Bonus bonus)
    {
        ValidateBonusPath(bonus.statType, bonus.bonusType);
        List<Bonus> list = bonusMap[bonus.statType][bonus.bonusType];
        if (list.Contains(bonus))
        {
            list.Remove(bonus);
        }
        Bonus max = GetHighestBonus(bonus.statType, bonus.bonusType);
        int delta = bonus.value - max.value;
        if (delta > 0)
        {
            stats[bonus.statType] -= delta;
        }
    }

    public Bonus GetHighestBonus(StatTypes s, BonusTypes b)
    {
        ValidateBonusPath(s, b);
        List<Bonus> list = bonusMap[s][b];
        Bonus retValue = list[0];

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].value > retValue.value)
            {
                retValue = list[i];
            }
        }
        return retValue;
    }

    void ValidateBonusPath(StatTypes s, BonusTypes b)
    {
        if (!bonusMap.ContainsKey(s))
        {
            bonusMap[s] = new Dictionary<BonusTypes, List<Bonus>>();
        }
        if (!bonusMap[s].ContainsKey(b))
        {
            bonusMap[s][b] = new List<Bonus>();
        }
        if (bonusMap[s][b].Count == 0)
        {
            bonusMap[s][b].Add(new Bonus(s, b, 0));
            //this.AddListener(OnStatChanged, Stats.WillChangeNotification(s), stats);
        }
    }

    //void OnStatChanged(object sender, object e)
    //{
    //    Info<StatTypes, ValueChangeException> info = e as Info<StatTypes, ValueChangeException>;
    //    StatTypes s = info.arg0;
    //    ValueChangeException vce = info.arg1;
    //    Dictionary<BonusTypes, List<Bonus>> bm = bonusMap[s];

    //    int bonus = 0;
    //    foreach(KeyValuePair<BonusTypes, List<Bonus>> kvp in bm)
    //    {
    //        bonus += GetHighestBonus(s, kvp.Key).value;
    //    }

    //    vce.AddModifier(new AddValueModifier(0, bonus));
    //}
}
