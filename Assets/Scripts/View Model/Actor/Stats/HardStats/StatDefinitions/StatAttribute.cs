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
            UpdateLinkers();
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
    }

    public void ClearLinkers()
    {
        statLinkers.Clear();
    }

    public void ScaleStat(int level)
    {
        _statLevelValue = level;
    }

    public void UpdateLinkers()
    {
        _statLinkerValue = 0f;
        for (int i = 0; i < statLinkers.Count; i++)
        {
            _statLinkerValue += statLinkers[i].value;
        }
    }
}
