using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceTestScript : MonoBehaviour
{
    GameObject hero;
    Roll HPGrowth;
    CharacterSheet sheet;

    int[] d20List = new int[20];
    int[] d40List = new int[15];

    void Awake()
    {
        TestRolls();
        Init();
        InitHero();
        LevelUp();
        Attack();
    }

    void TestRolls()
    {
        Debug.Log("=====D20=====");
        Roll d20 = new Roll(20);
        for (int i = 0; i < 100; i++)
        {
            d20List[d20.NewRoll() - 1]++;
        }
        for (int i = 0; i < d20List.Length; i++)
        {
            Debug.Log(string.Format("{0}:\t{1}", i + 1, d20List[i]));
        }
        Debug.Log("=====2D8+3=====");
        Roll d40 = new Roll(2, 8, 3);
        for (int i = 0; i < 100; i++)
        {
            d40List[d40.NewRoll() - 5]++;
        }
        for (int i = 0; i < d40List.Length; i++)
        {
            Debug.Log(string.Format("{0}:\t{1}", i + 5, d40List[i]));
        }
    }

    void Init()
    {
        DataController.instance.Load();
        InitHero();
    }

    void InitHero()
    {
        hero = new GameObject("Hero");
        sheet = hero.AddComponent<CharacterSheet>();
        sheet.stats.GetStat<Stat>(StatTypes.Stat_Max_HP).statBaseValue = 15f;
        sheet.stats.GetStat<Stat>(StatTypes.Ability_Score_Might).statBaseValue = 16f;
        sheet.stats.GetStat<Stat>(StatTypes.Ability_Score_Constitution).statBaseValue = 13f;
        this.AddListener(OnLevelUp, Level.LevelDidChange, sheet.level);
        HPGrowth = new Roll(1, 8, 2);
    }

    void LevelUp()
    {
        Debug.Log(string.Format("HP: {0}", sheet.stats[StatTypes.Stat_Max_HP]));
        sheet.level.SetExperience(sheet.level.ExperienceForLevel(2));
        sheet.level.SetExperience(sheet.level.ExperienceForLevel(3));
        sheet.level.SetExperience(sheet.level.ExperienceForLevel(4));
        sheet.level.SetExperience(sheet.level.ExperienceForLevel(5));
    }

    void Attack()
    {

    }

    void OnLevelUp(object sender, object e)
    {
        Debug.Log("Got to Level " + sheet.level.level);
        sheet.stats.GetStat<StatRange>(StatTypes.Stat_Max_HP).statBaseValue += HPGrowth.NewRoll();
        Debug.Log(string.Format("Rolled a {0} with 1d8+2", HPGrowth.value));
        Debug.Log(string.Format("HP: {0}", sheet.stats[StatTypes.Stat_Max_HP]));
    }
}
