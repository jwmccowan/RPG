using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    #region constants
    public const int minLevel = 1;
    public const int maxLevel = 5;
    public const int maxExperience = 3600000;
    // We'll need to load this when we get to a config file
    static int[] experienceChart = new int[maxLevel] {0, 2000, 5000, 9000, 15000};
    #endregion

    #region fields
    public int level
    {
        get { return stats[StatTypes.Level]; }
    }

    public int experience
    {
        get { return stats[StatTypes.Experience]; }
        set { stats[StatTypes.Experience] = value; }
    }

    Stats stats;
    #endregion

    #region MonoBehaviour
    void OnEnable()
    {
        this.AddListener(OnExpWillChange, Stats.WillChangeNotification(StatTypes.Experience), stats);
        this.AddListener(OnExpDidChange, Stats.DidChangeNotification(StatTypes.Experience), stats);
    }

    void OnDisable()
    {
        this.RemoveListener(OnExpWillChange, Stats.WillChangeNotification(StatTypes.Experience), stats);
        this.RemoveListener(OnExpDidChange, Stats.DidChangeNotification(StatTypes.Experience), stats);
    }

    void Awake()
    {
        stats = GetComponent<Stats>();
    }
    #endregion

    #region events
    void OnExpWillChange(object sender, object e)
    {
        ValueChangeException vce = e as ValueChangeException;
        vce.AddModifier(new ClampValueModifier(int.MaxValue, experience, maxExperience));
    }

    void OnExpDidChange(object sender, object e)
    {
        stats.SetValue(StatTypes.Level, LevelForExperience(experience), false);
    }
    #endregion

    #region public
    public static int ExperienceForLevel(int lvl)
    {
        lvl = Mathf.Clamp(lvl, minLevel, maxLevel);
        return experienceChart[lvl - 1];
    }

    public static int LevelForExperience(int exp)
    {
        for (int i = 0; i < experienceChart.Length; i++)
        {
            if (exp < experienceChart[i])
            {
                int lvl = Mathf.Clamp(i, minLevel, maxLevel);
                return lvl;
            }
        }
        return maxLevel;
    }

    public void Init(int lvl)
    {
        stats.SetValue(StatTypes.Level, lvl, false);
        stats.SetValue(StatTypes.Experience, ExperienceForLevel(lvl), false);
    }
    #endregion
}
