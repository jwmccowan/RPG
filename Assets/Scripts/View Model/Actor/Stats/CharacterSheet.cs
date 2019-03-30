using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterSheet : MonoBehaviour
{
    #region fields
    public Stats stats;
    AbilityScores abilityScores;
    DerivedStats derivedStats;
    BonusHandler bonusHandler;
    Level level;
    #endregion

    #region MonoBehaviour
    void OnEnable()
    {
        Init();
    }

    void Awake()
    {
        stats = gameObject.AddComponent<Stats>();
        abilityScores = gameObject.AddComponent<AbilityScores>();
        derivedStats = gameObject.AddComponent<DerivedStats>();
        bonusHandler = gameObject.AddComponent<BonusHandler>();
        level = gameObject.AddComponent<Level>(); 
    }
    #endregion

    #region events
    #endregion

    #region public
    public void AddBonus(Bonus bonus)
    {
        bonusHandler.AddBonus(bonus);
    }

    public void RemoveBonus(Bonus bonus)
    {
        bonusHandler.RemoveBonus(bonus);
    }

    public void AddClassLevel(ClassType type)
    {
        level.AddClassLevel(type);
    }
    #endregion

    #region private
    void Init()
    {
        stats[StatTypes.AC] += 10;
    }
    #endregion
}
