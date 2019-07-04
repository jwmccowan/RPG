using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatAbilityScorePercentModifiable
{
    float statAbilityScoreBonus { get; }

    void AddAbilityScorePercentModifier(Stat abilityScore, float ratio);
    void ChangeAbilityScorePercentModifier(Stat abilityScore, float ratio);
    void RemoveAbilityScorePercentModifier(Stat abilityScore);
    void UpdateAbilityScoreModifiers();
}
