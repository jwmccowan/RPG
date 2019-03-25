using System;
using SQLite4Unity3d;
using System.Collections.Generic;
using UnityEngine;

public class DataController
{
    public static Dictionary<StatTypes, StatTypes> abilityScoresBonuses = new Dictionary<StatTypes, StatTypes>();
    public static Dictionary<StatTypes, List<StatTypes>> derivedStats = new Dictionary<StatTypes, List<StatTypes>>();
    public static DataController instance = new DataController();
    public DatabaseService data;

    public void Load(Action complete = null)
    {
        data = new DatabaseService("db.sqlite");
        data.Load(SQLiteOpenFlags.ReadOnly, delegate(DatabaseService sender)
        {
            complete?.Invoke();
            GetAbilityScoreEntries();
            GetDerivedStats();
        }); 
    }

    void GetAbilityScoreEntries()
    {
        abilityScoresBonuses.Clear();

        var abilityScoreTable = data.connection.Table<AbilityScore>();
        foreach(AbilityScore ase in abilityScoreTable)
        {
            abilityScoresBonuses[(StatTypes)ase.stat_id] = (StatTypes)ase.bonus_id;
        }
    }

    void GetDerivedStats()
    {
        foreach (KeyValuePair<StatTypes, List<StatTypes>> kvp in derivedStats)
        {
            kvp.Value.Clear();
        }
        derivedStats.Clear();
        var derivedStatsTable = data.connection.Table<DerivedStat>();
        foreach(DerivedStat ds in derivedStatsTable)
        {
            StatTypes derivedFrom = (StatTypes)ds.derived_from;
            StatTypes baseStat = (StatTypes)ds.base_stat;
            if (!derivedStats.ContainsKey(derivedFrom))
            {
                derivedStats[derivedFrom] = new List<StatTypes>();
            }
            Debug.Log(baseStat.ToString() + " comes from " + derivedFrom.ToString());
            derivedStats[derivedFrom].Add(baseStat);
        }
    }

    private DataController()
    {

    }

    ~DataController()
    {
        data.connection.Close();
    }

    class DerivedStat
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public int base_stat { get; set; }
        public int derived_from { get; set; }
    }

    class AbilityScore
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public int stat_id { get; set; }
        public int bonus_id { get; set; }
    }
}
