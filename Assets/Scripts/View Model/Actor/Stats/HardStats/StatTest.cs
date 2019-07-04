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

        Debug.Log("====Adding 10 to base====");
        health.AddModifier(new StatModifierBaseAdd(10f));
        DisplayStats();

        Debug.Log("====Adding 30% to base====");
        health.AddModifier(new StatModifierBasePercent(.30f));
        DisplayStats();

        Debug.Log("====Adding 10% to total====");
        health.AddModifier(new StatModifierTotalPercent(.10f));
        DisplayStats();

        Debug.Log("====Adding 30 to total====");
        health.AddModifier(new StatModifierTotalAdd(30f));
        DisplayStats();

        Debug.Log("====Adding 30 armor to total====");
        health.AddModifier(new StatModifierTotalAdd(BonusTypes.Armor, 30f));
        DisplayStats();

        Debug.Log("====Adding 10 armor to total====");
        health.AddModifier(new StatModifierTotalAdd(BonusTypes.Armor, 10f));
        DisplayStats();

        Debug.Log("====Adding 50 armor to total====");
        health.AddModifier(new StatModifierTotalAdd(BonusTypes.Armor, 50f));
        DisplayStats();


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
