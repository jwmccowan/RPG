using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatModifiable
{
    float statModifierValue { get; }

    void AddModifier(StatModifier statModifier);
    void RemoveModifier(StatModifier statModifier);
    void ClearModifiers();
    void UpdateModifiers();
}
