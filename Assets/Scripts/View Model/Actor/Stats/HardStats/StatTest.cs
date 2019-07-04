using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatTest : MonoBehaviour
{
    private StatCollection stats;
    // Start is called before the first frame update
    void Start()
    {
        stats = new DefaultStats();

        DisplayStats();

        StatRange health = stats.GetStat<StatRange>(StatTypes.Stat_Max_HP);
        this.AddListener(OnHealthWillChange, StatCollection.StatCurrentValueWillChangeNotification, health);
        this.AddListener(OnHealthDidChange, StatCollection.StatCurrentValueDidChangeNotification, health);
        StatAttribute constitution = stats.GetStat<StatAttribute>(StatTypes.Ability_Score_Constitution);
        constitution.ScaleStat(5);

        health.SetCurrentValueToMax();
        health.currentValue -= 20f;
        health.currentValue -= 1000f;
        health.currentValue += 20000f;


        DisplayStats();
    }

    void OnHealthWillChange(object sender, object e)
    {
        StatRange health = sender as StatRange;
        Debug.Log(string.Format("Health will change from {0}", health.currentValue));
    }

    void OnHealthDidChange(object sender, object e)
    {
        StatRange health = sender as StatRange;
        Debug.Log(string.Format("Health did change to {0}", health.currentValue));
    }

    void DisplayStats()
    {
        for (int i = 0; i< (int) StatTypes.Count; i++)
        {
            var stat = stats.GetStat((StatTypes)i);
            if (stat != null)
            {
                Debug.Log(string.Format("{0}'s value is {1}", stat.statName, stat.statValue));
            }
        }
    }
}
