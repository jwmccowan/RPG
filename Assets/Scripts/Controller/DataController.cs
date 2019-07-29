using System;
using SQLite4Unity3d;
using System.Collections.Generic;
using UnityEngine;

public class DataController
{
    public static Dictionary<StatTypes, StatTypes> abilityScoresBonuses = new Dictionary<StatTypes, StatTypes>();
    public static Dictionary<StatTypes, List<StatTypes>> derivedStats = new Dictionary<StatTypes, List<StatTypes>>();
    //TOREMOVE:
    //public static Dictionary<ClassType, Classes> classes = new Dictionary<ClassType, Classes>();
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
            // TOREMOVE:
            //GetClasses();
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
            StatTypes baseStat = (StatTypes)ds.stat_id;
            if (!derivedStats.ContainsKey(derivedFrom))
            {
                derivedStats[derivedFrom] = new List<StatTypes>();
            }
            Debug.Log(baseStat.ToString() + " comes from " + derivedFrom.ToString());
            derivedStats[derivedFrom].Add(baseStat);
        }
    }

    // TOREMOVE:
    /*void GetClasses()
    {
        classes.Clear();
        var classesTable = data.connection.Table<Classes>();
        foreach(Classes c in classesTable)
        {
            classes[(ClassType)c.class_id] = c;
        }
    }*/

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
        public int stat_id { get; set; }
        public int derived_from { get; set; }
    }

    class AbilityScore
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public int stat_id { get; set; }
        public int bonus_id { get; set; }
    }

    // This should be deleted before checkin.
    // Only keeping for reference for when we replace
    /*public class Classes
    {
        [PrimaryKey, AutoIncrement]
        public int class_id { get; set; }
        public string name { get; set; }
        public int hit_die { get; set; }
        public int growth_rate { get; set; }
        public int fortitude_growth_rate { get; set; }
        public int reflex_growth_rate { get; set; }
        public int will_growth_rate { get; set; }
        public int skill_ranks { get; set; }
    }*/
}
