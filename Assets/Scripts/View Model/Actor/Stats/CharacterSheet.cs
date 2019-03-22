using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSheet : MonoBehaviour
{
    #region constants
    static Dictionary<StatTypes, StatTypes> abilityScoresBonuses = new Dictionary<StatTypes, StatTypes>()
    {
        { StatTypes.Strength, StatTypes.Strength_Bonus },
        { StatTypes.Dexterity, StatTypes.Dexterity_Bonus },
        { StatTypes.Constitution, StatTypes.Constitution_Bonus },
        { StatTypes.Intelligence, StatTypes.Intelligence_Bonus },
        { StatTypes.Wisdom, StatTypes.Wisdom_Bonus },
        { StatTypes.Charisma, StatTypes.Charisma_Bonus }
    };
    #endregion

    #region fields
    Stats stats;
    #endregion

    #region MonoBehaviour
    void OnEnable()
    {
        this.AddListener(OnAbilityScoreDidChange, Stats.DidChangeNotification(StatTypes.Strength), stats);
        this.AddListener(OnAbilityScoreDidChange, Stats.DidChangeNotification(StatTypes.Dexterity), stats);
        this.AddListener(OnAbilityScoreDidChange, Stats.DidChangeNotification(StatTypes.Constitution), stats);
        this.AddListener(OnAbilityScoreDidChange, Stats.DidChangeNotification(StatTypes.Intelligence), stats);
        this.AddListener(OnAbilityScoreDidChange, Stats.DidChangeNotification(StatTypes.Wisdom), stats);
        this.AddListener(OnAbilityScoreDidChange, Stats.DidChangeNotification(StatTypes.Charisma), stats);
    }

    void Awake()
    {
        stats = GetComponent<Stats>();
    }
    #endregion

    #region events
    void OnAbilityScoreDidChange(object sender, object e)
    {
        // We set all bonuses any time any ability score changes.  This is to save on code space.
        // Since we don't change ability scores that often, it seems a decent tradeoff.
        // If I see this to be a significant part to optimize, it should be done ASAP
        foreach (KeyValuePair<StatTypes, StatTypes> pair in abilityScoresBonuses)
        {
            SetBonus(pair.Key);
        }
    }
    #endregion

    #region public

    #endregion

    #region private
    void SetBonus(StatTypes s)
    {
        if (abilityScoresBonuses.ContainsKey(s))
        {
            int bonus = Mathf.FloorToInt((stats[s] - 10) / 2);
            stats.SetValue(abilityScoresBonuses[s], bonus, false);
        }
        else
        {
            Debug.LogError(string.Format("There isn't a corresponding ability bonus for type: {0}", s.ToString()));
        }   
    }
    #endregion
}
