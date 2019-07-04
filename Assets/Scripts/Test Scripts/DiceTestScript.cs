using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceTestScript : MonoBehaviour
{
    GameObject hero;
    Roll HPGrowth;
    Stats stats;
    Level level;

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
        CharacterSheet sheet = hero.AddComponent<CharacterSheet>();
        stats = sheet.stats;
        /*stats[StatTypes.HP_Increases] = 15;
        stats[StatTypes.Strength] = 16;
        */
        this.AddListener(OnLevelUp, Stats.DidChangeNotification(StatTypes.Level), stats);
        HPGrowth = new Roll(1, 8, 2);
    }

    void LevelUp()
    {
        Debug.Log(string.Format("HP: {0}", stats[StatTypes.Stat_Max_HP]));
        level.experience += Level.ExperienceForLevel(2);
        level.experience += Level.ExperienceForLevel(3);
        level.experience += Level.ExperienceForLevel(4);
        level.experience += Level.ExperienceForLevel(5);
    }

    void Attack()
    {

    }

    void OnLevelUp(object sender, object e)
    {
        Info<StatTypes, int> info = e as Info<StatTypes, int>;
        StatTypes s = info.arg0;
        Debug.Log("Got to Level " + stats[StatTypes.Level]);
        //stats[StatTypes.HP_Increases] += HPGrowth.NewRoll();
        Debug.Log(string.Format("Rolled a {0} with 1d8+2", HPGrowth.value));
        Debug.Log(string.Format("HP: {0}", stats[StatTypes.Stat_Max_HP]));
    }
}
