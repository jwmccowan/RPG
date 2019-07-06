using System.Collections.Generic;
using UnityEngine;

public class StatCollection : MonoBehaviour
{
    public static string ValueDidChange(StatTypes type)
    {
        return string.Format("StatCollection.{0}ValueDidChangeNotification", type.ToString());
    }
    public static string ValueWillChange(StatTypes type)
    {
        return string.Format("StatCollection.{0}ValueWillChangeNotification", type.ToString());
    }
    public static string CurrentValueDidChange(StatTypes type)
    {
        return string.Format("StatCollection.{0}CurrentValueDidChangeNotification", type.ToString());
    }
    public static string CurrentValueWillChange(StatTypes type)
    {
        return string.Format("StatCollection.{0}CurrentValueWillChangeNotification", type.ToString());
    }

    private Dictionary<StatTypes, Stat> _statCollection;

    public Dictionary<StatTypes, Stat> statCollection
    {
        get
        {
            if (_statCollection == null)
            {
                _statCollection = new Dictionary<StatTypes, Stat>();
                ConfigureStats();
            }
            return _statCollection;
        }
    }

    public float this[StatTypes s]
    {
        get
        {
            Stat stat = GetStat(s);
            if (stat != null)
            {
                return stat.statValue;
            }
            return -1f;
        }
    }

    protected virtual void ConfigureStats() { }

    public bool Contains(StatTypes s)
    {
        return statCollection.ContainsKey(s);
    }

    public Stat GetStat(StatTypes s)
    {
        if (Contains(s))
        {
            return statCollection[s];
        }
        return null;
    }

    public T GetStat<T>(StatTypes type) where T : Stat
    {
        return GetStat(type) as T;
    }

    protected T CreateStat<T>(StatTypes s) where T : Stat
    {
        T stat = System.Activator.CreateInstance<T>();
        stat.statType = s;
        statCollection.Add(s, stat);
        return stat;
    }

    protected T GetOrCreateStat<T>(StatTypes s) where T : Stat
    {
        T stat = GetStat<T>(s);
        if (stat == null)
        {
            stat = CreateStat<T>(s);
        }
        return stat;
    }

    public void AddStatModifier(StatTypes statType, StatModifier modifier)
    {
        if (!Contains(statType))
        {
            Debug.LogError(string.Format("Trying to add modifier to {0}, which doesn't exist.", statType.ToString()));
            return;
        }
        var stat = GetStat(statType) as IStatModifiable;
        if (stat == null)
        {
            Debug.LogError(string.Format("Trying to add modifier to {0}, which isn't modifiable.", statType.ToString()));
            return;
        }
        stat.AddModifier(modifier);
    }

    public void RemoveStatModifier(StatTypes statType, StatModifier modifier)
    {
        if (!Contains(statType))
        {
            Debug.LogError(string.Format("Trying to remove modifier to {0}, which doesn't exist.", statType.ToString()));
            return;
        }
        var stat = GetStat(statType) as IStatModifiable;
        if (stat == null)
        {
            Debug.LogError(string.Format("Trying to remove modifier to {0}, which isn't modifiable.", statType.ToString()));
            return;
        }
        stat.RemoveModifier(modifier);
    }

    public void ClearStatModifiers(StatTypes statType)
    {
        if (!Contains(statType))
        {
            Debug.LogError(string.Format("Trying to clear modifiers for {0}, which doesn't exist.", statType.ToString()));
            return;
        }
        var stat = GetStat(statType) as StatModifiable;
        if (stat == null)
        {
            Debug.LogError(string.Format("Trying to clear modifiers for {0}, which isn't modifiable.", statType.ToString()));
            return;
        }
        stat.ClearModifiers();
    }

    public void ClearAllStatModifiers()
    {
        foreach (var key in statCollection.Keys)
        {
            ClearStatModifiers(key);
        }
    }

    public void ScaleStat(StatTypes statType, int level)
    {
        if (!Contains(statType))
        {
            Debug.LogError(string.Format("Trying to scale {0}, which doesn't exist.", statType.ToString()));
            return;
        }
        var stat = GetStat(statType) as IStatScalable;
        if (stat == null)
        {
            Debug.LogError(string.Format("Trying to scale {0}, which isn't scalable.", statType.ToString()));
            return;
        }
        stat.ScaleStat(level);
    }

    public void ScaleStatCollection(int level)
    {
        foreach (var key in statCollection.Keys)
        {
            ScaleStat(key, level);
        }
    }
}
