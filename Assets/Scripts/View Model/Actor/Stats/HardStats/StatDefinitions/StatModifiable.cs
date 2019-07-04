using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifiable : Stat, IStatModifiable
{
    private List<StatModifier> modifierList;
    private float _statModifierValue;

    public float statModifierValue
    {
        get { return _statModifierValue; }
    }

    public override float statValue
    {
        get { return statBaseValue + statModifierValue; }
    }

    public StatModifiable()
    {
        modifierList = new List<StatModifier>();
        _statModifierValue = 0f;
    }

    public void AddModifier(StatModifier statModifier)
    {
        modifierList.Add(statModifier);
        UpdateModifiers();
    }

    public void ClearModifiers()
    {
        modifierList.Clear();
    }

    public void UpdateModifiers()
    {
        float newStatModifierValue = 0f;
        float baseAddValue = 0f;
        float basePercentValue = 0f;
        float totalAddValue = 0f;
        float totalPercentValue = 0f;

        for (int i = 0; i < modifierList.Count; i++)
        {
            switch (modifierList[i].type)
            {
                case StatModifier.Types.BaseValueAdd:
                    baseAddValue += modifierList[i].value;
                    break;
                case StatModifier.Types.BaseValuePercent:
                    basePercentValue += modifierList[i].value;
                    break;
                case StatModifier.Types.TotalValueAdd:
                    totalAddValue += modifierList[i].value;
                    break;
                case StatModifier.Types.TotalValuePercent:
                    totalPercentValue += modifierList[i].value;
                    break;
            }
        }

        newStatModifierValue = (statBaseValue * basePercentValue) + baseAddValue;
        newStatModifierValue += (statValue * totalPercentValue) + totalAddValue;

        this.PostNotification(StatCollection.StatValueWillChangeNotification);
        _statModifierValue = newStatModifierValue;
        this.PostNotification(StatCollection.StatValueDidChangeNotification);
    }
}
