using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatAbilityScore : StatAttribute
{
    private float _statAbilityScoreBonus;

    public float statAbilityScoreBonus
    {
        get
        {
            return statValue - 10;
        }
    }

    public StatAbilityScore() { }
}
