using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityScores : MonoBehaviour
{
    #region constants
    static StatTypes[] abilityScores = new StatTypes[6] 
    {
        StatTypes.Strength,
        StatTypes.Dexterity,
        StatTypes.Constitution,
        StatTypes.Intelligence,
        StatTypes.Wisdom,
        StatTypes.Charisma
    };
    #endregion

    #region fields
    Stats stats;
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        stats = GetComponent<Stats>();
        Init();
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
    #endregion

    #region public

    #endregion

    #region private
    void Init()
    {
        foreach (KeyValuePair<StatTypes, StatTypes> kvp in DataController.abilityScoresBonuses)
        {
            SetAbilityScoreBonus(kvp.Key);
            this.AddListener(OnAbilityScoreChange, Stats.DidChangeNotification(kvp.Key), stats);
            stats[kvp.Key] = 10;
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

    void RemoveListeners()
    {
        foreach (KeyValuePair<StatTypes, StatTypes> kvp in DataController.abilityScoresBonuses)
        {
            this.RemoveListener(OnAbilityScoreChange, Stats.DidChangeNotification(kvp.Key), stats);
        }
    }
    #endregion
}
