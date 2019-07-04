using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Party = System.Collections.Generic.List<UnityEngine.GameObject>;

public class TestLevelGrowth : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        DataController.instance.Load(null);
        this.AddListener(OnLevelChange, Stats.DidChangeNotification(StatTypes.Level));
        this.AddListener(OnExperienceException, Stats.WillChangeNotification(StatTypes.Experience));
    }

    // Update is called once per frame
    void OnDisable()
    {
        this.RemoveListener(OnLevelChange, Stats.DidChangeNotification(StatTypes.Level));
        this.RemoveListener(OnExperienceException, Stats.WillChangeNotification(StatTypes.Experience));
    }

    void Start()
    {
        VerifyLevelToExperienceCalculations();
        VerifySharedExperienceDistribution();
    }

    void VerifyLevelToExperienceCalculations()
    {
        for (int i = 1; i < 6; ++i)
        {
            int expLvl = Level.ExperienceForLevel(i);
            int lvlExp = Level.LevelForExperience(expLvl);

            if (lvlExp != i)
            {
                Debug.LogError(string.Format("Mismatch on level:{0} with xp:{1} returned:{2}", i, expLvl, lvlExp));
            }
            else
            {
                Debug.Log(string.Format("Level:{0} = Exp:{1}", lvlExp, expLvl));
            }
        }
    }

    /*void VerifySingleExperienceDistribution()
    {
        Party heroes = new Party();
        for (int i = 0; i < 3; i++)
        {
        }
        GameObject actor1 = new GameObject("Hello There" + 1);
        actor1.AddComponent<Stats>();
        Level level1 = actor1.AddComponent<Level>();
        level1.Init(0 + 1);
        heroes.Add(actor1);
        GameObject actor2 = new GameObject("Hello There" + 2);
        actor2.AddComponent<Stats>();
        Level level2 = actor2.AddComponent<Level>();
        level2.Init(1 + 1);
        heroes.Add(actor2);

        Debug.Log("===== Before Adding Experience =====");
        LogParty(heroes);

        Debug.Log("=====================================");
        ExperienceController.AwardExperience(800, heroes);

        Debug.Log("===== After Adding Experience ======");
        LogParty(heroes);
    }

    void OnExperienceException(object sender, object e)
    {
        ValueChangeException vce = e as ValueChangeException;
        Debug.Log(vce.fromValue + "   " + vce.toValue);
        vce.AddModifier(new AddValueModifier(0, 200));
    }*/

    void VerifySharedExperienceDistribution()
    {
        string[] names = new string[]
        {
            "Finn",
            "Petra",
            "Barn",
            "Bakim",
            "Moro",
            "Azuris"
        };

        Party heroes = new Party();
        for (int i = 0; i < names.Length; i++)
        {
            GameObject actor = new GameObject(names[i]);
            actor.AddComponent<Stats>();
            Level level = actor.AddComponent<Level>();
            level.Init((int)Random.Range(1, 5));
            actor.AddComponent<CharacterSheet>();
            heroes.Add(actor);
        }

        Debug.Log("===== Before Adding Experience =====");
        LogParty(heroes);

        Debug.Log("=====================================");
        ExperienceController.AwardExperience(2000, heroes);

        Debug.Log("===== After Adding Experience ======");
        LogParty(heroes);
    }

    void LogParty(Party p)
    {
        for (int i = 0; i < p.Count; i++)
        {
            GameObject actor = p[i];
            Level level = actor.GetComponent<Level>();
            Stats stats = actor.GetComponent<Stats>();
            Debug.Log(string.Format("Name: {0}\tLevel: {1}\tExp: {2}", actor.name, level.level, level.experience));
            Debug.Log(string.Format("Perception: {0}\tAccuracy: {1}", stats[StatTypes.Ability_Score_Perception], stats[StatTypes.Stat_Accuracy]));
            Debug.Log(string.Format("Mobility: {0}\tDeflection: {1}\tDexterityBonusToAC: {2}\tAC: {3}", stats[StatTypes.Ability_Score_Mobility], stats[StatTypes.Stat_Deflection]));
        }
    }

    void OnLevelChange(object sender, object args)
    {
        Stats stats = sender as Stats;
        Debug.Log(stats.name + " leveled up!");
    }

    void OnExperienceException(object sender, object args)
    {
        GameObject actor = (sender as Stats).gameObject;
        Info<StatTypes, ValueChangeException> info = args as Info<StatTypes, ValueChangeException>;
        ValueChangeException vce = info.arg1;
        int roll = Random.Range(0, 5);
        switch (roll)
        {
            case 0:
                Debug.Log(string.Format("{0} would have received {1} experiece, but we stopped it.", actor.name, vce.delta));
                vce.FlipToggle();
                break;
            case 1:
                Debug.Log(string.Format("{0} would have received {1} experiece, but we added 1000.", actor.name, vce.delta));
                vce.AddModifier(new AddValueModifier(0, 1000));
                break;
            case 2:
                Debug.Log(string.Format("{0} would have received {1} experiece, but we multiplied by two.", actor.name, vce.delta));
                vce.AddModifier(new MultDeltaValueModifier(0, 2));
                break;
            default:
                Debug.Log(string.Format("{0} will receive {1} experiece.", actor.name, vce.delta));
                break;
        }
    }
}
