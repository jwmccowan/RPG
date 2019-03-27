using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterSheet : MonoBehaviour
{
    #region fields
    Stats stats;
    #endregion

    #region MonoBehaviour
    void OnEnable()
    {
        Init();
    }

    void Awake()
    {
        stats = GetComponent<Stats>();
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
        SetBonus(info.arg0);
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
        InitValues();
        InitAbilityScores();
        InitDerivedStats();
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
            SetBonus(kvp.Key);
            this.AddListener(OnAbilityScoreChange, Stats.DidChangeNotification(kvp.Key), stats);
            stats[kvp.Key] = 14;
        }
    }

    void InitDerivedStats()
    {
        foreach (KeyValuePair<StatTypes, List<StatTypes>> kvp in DataController.derivedStats)
        {
            SetDerivedStat(kvp.Key, stats[kvp.Key]);
            this.AddListener(OnDerivedStatChange, Stats.DidChangeNotification(kvp.Key), stats);
        }
    }

    void InitValues()
    {
        stats.SetValue(StatTypes.AC, 10, false);
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
                this.RemoveListener(OnDerivedStatChange, Stats.DidChangeNotification(kvp.Key), stats);
            }
        }
    }

    void SetDerivedStat(StatTypes baseStat, int delta)
    {
        if (DataController.derivedStats.ContainsKey(baseStat))
        {
            foreach (StatTypes stat in DataController.derivedStats[baseStat])
            {
                stats[stat] += delta;
            }
        }
        else
        {
            Debug.LogError(string.Format("There isn't a corresponding derived stat bonus for type: {0}", baseStat.ToString()));
        }
    }

    void SetBonus(StatTypes s)
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
    #endregion
}
