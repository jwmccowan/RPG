using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
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
    }
    #endregion

    #region events

    #endregion

    #region public

    #endregion

    #region private

    #endregion
}
