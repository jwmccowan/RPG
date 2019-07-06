using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For now, the Attribute type means that you gain a point every level.
/// I don't think this is going to be permanent, as I don't plan to gain 
/// attributes per level, but as a means of implementing as many stat
/// types as possible, here we are
/// </summary>
public class StatAttribute : StatModifiable, IStatScalable, IStatLinkable
{
    private float _statLevelValue;
    private float _statLinkerValue;

    public float levelValueRatio;

    private List<StatLinker> statLinkers;

    public override float statBaseValue
    {
        get { return (base.statBaseValue + statLinkerValue + (statLevelValue * levelValueRatio)); }
    }

    public float statLevelValue
    {
        get { return _statLevelValue; }
    }

    public float statLinkerValue
    {
        get { return _statLinkerValue; }
    }

    public StatAttribute()
    {
        statLinkers = new List<StatLinker>();
        levelValueRatio = 0f;
    }

    public void AddLinker(StatLinker statLinker)
    {
        statLinkers.Add(statLinker);
        this.AddListener(OnLinkedStatValueChanged, StatLinker.LinkedValueDidChange, statLinker);
        UpdateLinkers();
    }

    public void RemoveLinker(StatLinker statLinker)
    {
        statLinkers.Remove(statLinker);
        statLinker.RemoveListener(OnLinkedStatValueChanged, StatLinker.LinkedValueDidChange);
    }

    public void ClearLinkers()
    {
        foreach (StatLinker statLinker in statLinkers)
        {
            statLinker.RemoveListener(OnLinkedStatValueChanged, StatLinker.LinkedValueDidChange);
        }
        statLinkers.Clear();
    }

    public void ScaleStat(int level)
    {
        if (_statLevelValue != level)
        {
            float oldValue = statValue;
            _statLevelValue = level;
            PostValueDidChange(oldValue);
        }
    }

    private void OnLinkedStatValueChanged(object sender, object e)
    {
        UpdateLinkers();
    }

    public void UpdateLinkers()
    {
        float oldStatLinkerValue = _statLinkerValue;
        _statLinkerValue = 0f;
        for (int i = 0; i < statLinkers.Count; i++)
        {
            _statLinkerValue += statLinkers[i].value;
        }
        if (oldStatLinkerValue != _statLinkerValue)
        {
            PostValueDidChange(statBaseValue + oldStatLinkerValue);
        }
    }

    private void PostValueDidChange(float oldValue)
    {
        owner.PostNotification(StatCollection.ValueDidChange(statType), new StatValueChangeArgs(statType, oldValue, statValue));
    }
}
