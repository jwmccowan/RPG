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

    private List<StatLinker> statLinkers;

    public float statLevelValue
    {
        get { return _statLevelValue; }
    }

    public override float statBaseValue
    {
        get { return base.statBaseValue + statLevelValue + statLinkerValue; }
    }

    public float statLinkerValue
    {
        get
        {
            return _statLinkerValue;
        }
    }

    public StatAttribute()
    {
        statLinkers = new List<StatLinker>();
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
        _statLevelValue = level;
        PostValueDidChange();
    }

    private void OnLinkedStatValueChanged(object sender, object e)
    {
        UpdateLinkers();
    }

    public void UpdateLinkers()
    {
        _statLinkerValue = 0f;
        for (int i = 0; i < statLinkers.Count; i++)
        {
            _statLinkerValue += statLinkers[i].value;
        }
        PostValueDidChange();
    }

    private void PostValueDidChange()
    {
        this.PostNotification(StatCollection.StatValueDidChangeNotification);
    }
}
