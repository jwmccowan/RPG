using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatTest : MonoBehaviour
{
    private CharacterSheet sheet;
    // Start is called before the first frame update
    void Start()
    {
        sheet = gameObject.AddComponent<CharacterSheet>();
        StatCollection stats = sheet.stats;

        DisplayStats();

        StatRange health = stats.GetStat<StatRange>(StatTypes.Stat_Max_HP);
        this.AddListener(OnHealthWillChange, StatCollection.CurrentValueWillChange(health.statType), stats);
        this.AddListener(OnHealthDidChange, StatCollection.CurrentValueDidChange(health.statType), stats);
        StatAttribute constitution = stats.GetStat<StatAttribute>(StatTypes.Ability_Score_Constitution);
        constitution.statBaseValue = 18f;

        DisplayStats();

        health.SetCurrentValueToMax();
        health.currentValue -= 20f;
        health.currentValue -= 1000f;
        health.currentValue += 20000f;
        health.currentValue -= 20f;

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
        health.AddModifier(new StatModifierTotalAdd(30f, BonusTypes.Armor));
        DisplayStats();

        Debug.Log("====Adding 10 armor to total====");
        health.AddModifier(new StatModifierTotalAdd(10f, BonusTypes.Armor));
        DisplayStats();

        Debug.Log("====Adding 50 armor to total====");
        health.AddModifier(new StatModifierTotalAdd(50f, BonusTypes.Armor));
        DisplayStats();

        DisplayStats();
    }

    void OnHealthWillChange(object sender, object e)
    {
        StatRange health = e as StatRange;
        Debug.Log(string.Format("Health will change from {0}", health.currentValue));
    }

    void OnHealthDidChange(object sender, object e)
    {
        StatRange health = e as StatRange;
        Debug.Log(string.Format("Health did change to {0}", health.currentValue));
    }

    void DisplayStats()
    {
        Debug.Log("=======================");
        Debug.Log("Displaying Cooper's Stats");
        Debug.Log(string.Format("Level: {0}, Experience: {1}/{2}", sheet.level.level, sheet.level.experience, sheet.level.ExperienceForLevel(sheet.level.level + 1)));
        for (int i = 0; i< (int) StatTypes.Count; i++)
        {
            var stat = sheet.stats.GetStat((StatTypes)i);
            if (stat != null)
            {
                Debug.Log(string.Format("{0}'s value is {1}", stat.statName, stat.statValue));
            }
        }
        Debug.Log("========================");
    }
}
