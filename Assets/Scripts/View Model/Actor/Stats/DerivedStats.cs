using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DerivedStats : MonoBehaviour
{
    #region constants
    #endregion

    #region fields
    Stats stats;
    #endregion

    #region MonoBehaviour
    void Awake()
    {
        stats = GetComponent<Stats>();
        Init();
    }

    void OnDisable()
    {
        RemoveListeners();
    }
    #endregion

    #region events
    void OnConstitutionChanged(object sender, object e)
    {
        stats[StatTypes.Stat_Max_HP] = Mathf.FloorToInt(stats[StatTypes.Ability_Score_Constitution] * 0.03f);
    }

    //void OnPerceptionChanged

    void OnDerivedStatChange(object sender, object e)
    {
        Info<StatTypes, int> info = (Info<StatTypes, int>)e;
        int delta = stats[info.arg0] - info.arg1;
        SetDerivedStat(info.arg0, delta);
    }
    #endregion

    #region public

    #endregion

    #region private
    void Init()
    {
        foreach (KeyValuePair<StatTypes, List<StatTypes>> kvp in DataController.derivedStats)
        {
            this.AddListener(OnDerivedStatChange, Stats.DidChangeNotification(kvp.Key), stats);
            SetDerivedStat(kvp.Key, stats[kvp.Key]);
        }
    }

    void RemoveListeners()
    {
        foreach (KeyValuePair<StatTypes, List<StatTypes>> kvp in DataController.derivedStats)
        {
            foreach (StatTypes stat in kvp.Value)
            {
                this.RemoveListener(OnDerivedStatChange, Stats.DidChangeNotification(stat), stats);
            }
        }
    }

    void SetDerivedStat(StatTypes derivedFrom, int delta)
    {
        if (DataController.derivedStats.ContainsKey(derivedFrom))
        {
            foreach (StatTypes stat in DataController.derivedStats[derivedFrom])
            {
                stats[stat] += delta;
            }
        }
        else
        {
            Debug.LogError(string.Format("There isn't a corresponding base stat bonus for type: {0}", derivedFrom.ToString()));
        }
    }
    #endregion
}
