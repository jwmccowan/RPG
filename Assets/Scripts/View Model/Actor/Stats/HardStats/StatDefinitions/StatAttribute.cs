using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For now, the Attribute type means that you gain a point every level.
/// I don't think this is going to be permanent, as I don't plan to gain 
/// attributes per level, but as a means of implementing as many stat
/// types as possible, here we are
/// </summary>
public class StatAttribute : StatModifiable, IStatScalable, IStatLinkable, IStatAbilityScorePercentModifiable
{
    private float _statLevelValue;
    private float _statLinkerValue;
    private float _statAbilityScoreBonus;

    private List<StatLinker> statLinkers;
    private Dictionary<Stat, float> abilityScoreModifiers;

    public override float statBaseValue
    {
        get { return (base.statBaseValue + statLevelValue + statLinkerValue) * (1 + statAbilityScoreBonus); }
    }

    public float statLevelValue
    {
        get { return _statLevelValue; }
    }

    public float statLinkerValue
    {
        get { return _statLinkerValue; }
    }

    public float statAbilityScoreBonus
    {
        get { return _statAbilityScoreBonus; }
    }

    public StatAttribute()
    {
        statLinkers = new List<StatLinker>();
        abilityScoreModifiers = new Dictionary<Stat, float>();
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
            _statLevelValue = level;
            PostValueDidChange();
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
            PostValueDidChange();
        }
    }

    public void AddAbilityScorePercentModifier(Stat abilityScore, float ratio)
    {
        if (abilityScoreModifiers.ContainsKey(abilityScore))
        {
            ChangeAbilityScorePercentModifier(abilityScore, ratio);
        }
        abilityScoreModifiers.Add(abilityScore, ratio);
        this.AddListener(OnAbilityScoreChanged, StatCollection.StatValueDidChangeNotification, abilityScore);
        UpdateAbilityScoreModifiers();
    }

    public void ChangeAbilityScorePercentModifier(Stat abilityScore, float ratio)
    {
        abilityScoreModifiers[abilityScore] = ratio;
        UpdateAbilityScoreModifiers();
    }

    public void RemoveAbilityScorePercentModifier(Stat abilityScore)
    {
        if (abilityScoreModifiers.ContainsKey(abilityScore))
        {
            abilityScoreModifiers.Remove(abilityScore);
        }
        this.RemoveListener(OnAbilityScoreChanged, StatCollection.StatValueDidChangeNotification, abilityScore);
        UpdateAbilityScoreModifiers();
    }

    public void UpdateAbilityScoreModifiers()
    {
        _statAbilityScoreBonus = 0f;
        foreach (var key in abilityScoreModifiers.Keys)
        {
            _statAbilityScoreBonus += (abilityScoreModifiers[key] * (key.statValue - 10f));
        }
        Debug.Log("eggs " + _statAbilityScoreBonus);
    }

    public void OnAbilityScoreChanged(object sender, object e)
    {
        UpdateAbilityScoreModifiers();
    }

    private void PostValueDidChange()
    {
        this.PostNotification(StatCollection.StatValueDidChangeNotification);
    }
}
