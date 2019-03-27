using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterSheet : MonoBehaviour
{
    #region fields
    Stats stats;
    Dictionary<StatTypes, Dictionary<BonusTypes, List<Bonus>>> bonusMap;
    #endregion

    #region MonoBehaviour
    void OnEnable()
    {
        Init();
    }

    void Awake()
    {
        stats = GetComponent<Stats>();
        bonusMap = new Dictionary<StatTypes, Dictionary<BonusTypes, List<Bonus>>>();
    }

    void OnDisable()
    {
        RemoveListeners();
    }
    #endregion

    #region events
    void OnAbilityScoreChange(object sender, object e)
    {
        Info<StatTypes, int> info = (Info<StatTypes, int>)e;
        SetAbilityScoreBonus(info.arg0);
    }

    void OnDerivedStatChange(object sender, object e)
    {
        Info<StatTypes, int> info = (Info<StatTypes, int>)e;
        int delta = stats[info.arg0] - info.arg1;
        SetDerivedStat(info.arg0, delta);
    }
    #endregion

    #region private
    void Init()
    {
        InitAbilityScores();
        InitDerivedStats();
        InitValues();
    }

    void RemoveListeners()
    {
        RemoveAbilityScores();
        RemoveDerivedStats();
    }

    void InitAbilityScores()
    {
        foreach (KeyValuePair<StatTypes, StatTypes> kvp in DataController.abilityScoresBonuses)
        {
            SetAbilityScoreBonus(kvp.Key);
            this.AddListener(OnAbilityScoreChange, Stats.DidChangeNotification(kvp.Key), stats);
            stats[kvp.Key] = 10;
        }
    }

    void InitDerivedStats()
    {
        foreach (KeyValuePair<StatTypes, List<StatTypes>> kvp in DataController.derivedStats)
        {
            this.AddListener(OnDerivedStatChange, Stats.DidChangeNotification(kvp.Key), stats);
            SetDerivedStat(kvp.Key, stats[kvp.Key]);
        }
    }

    void InitValues()
    {
        stats[StatTypes.AC] += 10;
    }

    void RemoveAbilityScores()
    {
        foreach (KeyValuePair<StatTypes, StatTypes> kvp in DataController.abilityScoresBonuses)
        {
            this.RemoveListener(OnAbilityScoreChange, Stats.DidChangeNotification(kvp.Key), stats);
        }
    }

    void RemoveDerivedStats() 
    {
        foreach (KeyValuePair<StatTypes, List<StatTypes>> kvp in DataController.derivedStats)
        {
            foreach (StatTypes stat in kvp.Value)
            {
                this.RemoveListener(OnDerivedStatChange, Stats.DidChangeNotification(stat), stats);
            }
        }
    }

    void SetDerivedStat(StatTypes derivedFrom, int delta)
    {
        if (DataController.derivedStats.ContainsKey(derivedFrom))
        {
            foreach (StatTypes stat in DataController.derivedStats[derivedFrom])
            {
                stats[stat] += delta;
            }
        }
        else
        {
            Debug.LogError(string.Format("There isn't a corresponding base stat bonus for type: {0}", derivedFrom.ToString()));
        }
    }

    void SetAbilityScoreBonus(StatTypes s)
    {
        if (DataController.abilityScoresBonuses.ContainsKey(s))
        {
            int bonus = Mathf.FloorToInt((stats[s] - 10) / 2);
            stats.SetValue(DataController.abilityScoresBonuses[s], bonus, false);
        }
        else
        {
            Debug.LogError(string.Format("There isn't a corresponding ability bonus for type: {0}", s.ToString()));
        }
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
    #endregion
}
