using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Party = System.Collections.Generic.List<UnityEngine.GameObject>;

public class TestLevelGrowth : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        this.AddListener(OnLevelChange, Level.LevelDidChange);
        this.AddListener(OnExperienceException, Level.ExperienceWillChange);
    }

    // Update is called once per frame
    void OnDisable()
    {
        this.RemoveListener(OnLevelChange, Level.LevelDidChange);
        this.RemoveListener(OnExperienceException, Level.ExperienceWillChange);
    }

    void Start()
    {
        VerifyLevelToExperienceCalculations();
        VerifySharedExperienceDistribution();
    }

    void VerifyLevelToExperienceCalculations()
    {
        Level level = gameObject.AddComponent<Level>();
        for (int i = 1; i < 6; ++i)
        {
            int expLvl = level.ExperienceForLevel(i);
            int lvlExp = level.LevelForExperience(expLvl);

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

    void VerifySingleExperienceDistribution()
    {
        Party heroes = new Party();
        for (int i = 0; i < 3; i++)
        {
        }
        GameObject actor1 = new GameObject("Hello There" + 1);
        CharacterSheet sheet1 = actor1.AddComponent<CharacterSheet>();
        sheet1.level.SetLevel(0 + 1);
        heroes.Add(actor1);
        GameObject actor2 = new GameObject("Hello There" + 2);
        CharacterSheet sheet2 = actor2.AddComponent<CharacterSheet>();
        sheet2.level.SetLevel(1 + 1);
        heroes.Add(actor2);

        Debug.Log("===== Before Adding Experience =====");
        LogParty(heroes);

        Debug.Log("=====================================");
        ExperienceController.AwardExperience(800, heroes);

        Debug.Log("===== After Adding Experience ======");
        LogParty(heroes);
    }

    void OnExperienceException1(object sender, object e)
    {
        ValueChangeException vce = e as ValueChangeException;
        Debug.Log(vce.fromValue + "   " + vce.toValue);
        vce.AddModifier(new AddValueModifier(0, 200));
    }

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
            actor.AddComponent<CharacterSheet>();
            Level level = actor.GetComponent<Level>();
            level.SetLevel((int)Random.Range(1, 5));
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
            CharacterSheet sheet = actor.GetComponent<CharacterSheet>();
            Debug.Log(string.Format("Name: {0}\tLevel: {1}\tExp: {2}", actor.name, sheet.level.level, sheet.level.experience));
            Debug.Log(string.Format("Perception: {0}\tAccuracy: {1}", sheet.stats[StatTypes.Ability_Score_Perception], sheet.stats[StatTypes.Stat_Accuracy]));
            Debug.Log(string.Format("Mobility: {0}\tDeflection: {1}", sheet.stats[StatTypes.Ability_Score_Mobility], sheet.stats[StatTypes.Stat_Deflection]));
        }
    }

    void OnLevelChange(object sender, object args)
    {
        Level level = sender as Level;
        Debug.Log(level.name + " leveled up!");
    }

    void OnExperienceException(object sender, object args)
    {
        GameObject actor = (sender as Level).gameObject;
        ValueChangeException vce = args as ValueChangeException;
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
