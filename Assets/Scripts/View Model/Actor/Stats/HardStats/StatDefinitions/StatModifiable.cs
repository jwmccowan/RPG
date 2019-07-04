using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        this.AddListener(OnModifierChanged, StatModifier.ValueDidChange, statModifier);
        UpdateModifiers();
    }

    public void RemoveModifier(StatModifier statModifier)
    {
        modifierList.Remove(statModifier);
        this.RemoveListener(OnModifierChanged, StatModifier.ValueDidChange, statModifier);
        UpdateModifiers();
    }

    public void ClearModifiers()
    {
        for (int i = 0; i < modifierList.Count; i++)
        {
            this.RemoveListener(OnModifierChanged, StatModifier.ValueDidChange, modifierList[i]);
        }
        modifierList.Clear();
    }

    public void UpdateModifiers()
    {
        float newStatModifierValue = 0f;

        var orderedModifiers = modifierList.OrderBy(m => m.order).GroupBy(m => m.order);
        foreach (var group in orderedModifiers)
        {
            float untypedTotal = 0f;
            float typedTotal = 0f;

            var bonusGroupedModifiers = group.GroupBy(m => m.bonusType);

            foreach (var bonusGroup in bonusGroupedModifiers)
            {
                float max = 0;

                foreach (StatModifier mod in bonusGroup)
                {
                    if (mod.bonusType == BonusTypes.Untyped)
                    {
                        untypedTotal += mod.value;
                    }
                    else
                    {
                        max = Mathf.Max(mod.value, max);
                    }
                }

                typedTotal += max;
            }

            newStatModifierValue = group.First().ApplyModifier(statBaseValue + newStatModifierValue, untypedTotal + typedTotal);
        }

        this.PostNotification(StatCollection.StatValueWillChangeNotification);
        _statModifierValue = newStatModifierValue;
        this.PostNotification(StatCollection.StatValueDidChangeNotification);
    }

    private void OnModifierChanged(object sender, object e)
    {
        UpdateModifiers();
    }
}
