using System.Collections.Generic;

public class StatCollection
{
    public static string StatValueWillChangeNotification = "StatCollection.StatValueWillChangeNotification";
    public static string StatValueDidChangeNotification = "StatCollection.StatValueDidChangeNotification";
    public static string StatCurrentValueWillChangeNotification = "StatCollection.StatCurrentValueWillChangeNotification";
    public static string StatCurrentValueDidChangeNotification = "StatCollection.StatCurrentValueDidChangeNotification";

    private Dictionary<StatTypes, Stat> statCollection;

    public StatCollection()
    {
        statCollection = new Dictionary<StatTypes, Stat>();
        ConfigureStats();
    }

    protected virtual void ConfigureStats()
    {

    }

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
}
