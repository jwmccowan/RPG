using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    #region constants
    private int _level;
    private int _minLevel;
    private int _maxLevel;

    private int _experience;

    // We'll need to load this when we get to a config file
    static int[] experienceChart = new int[5] {0, 2000, 5000, 9000, 15000};
    public const string LevelDidChange = "Level.LevelDidChange";
    public const string LevelWillChange = "Level.LevelWillChange";
    public const string ExperienceWillChange = "Level.ExperienceWillChange";
    public const string ExperienceDidChange = "Level.ExperienceDidChange";
    public const string CanLevelUp = "Level.CanLevelUp";
    #endregion

    #region fields
    public int level
    {
        get { return _level; }
        set { _level = value; }
    }

    public int minLevel
    {
        get { return _minLevel; }
        set { _minLevel = value; }
    }

    public int maxLevel
    {
        get { return _maxLevel; }
        set { _maxLevel = value; }
    }

    public int experience
    {
        get { return _experience; }
        set { _experience = value; }
    }
    
    #endregion

    #region MonoBehaviour

    void Awake()
    {
        minLevel = 1;
        maxLevel = 5;
    }
    #endregion

    #region events
    
    #endregion

    #region public
    public void AddExperience(int amount)
    {
        SetExperience(experience + amount);
    }

    public void SetExperience(int amount)
    {
        ValueChangeException vce = new ValueChangeException(experience, amount);
        this.PostNotification(ExperienceWillChange, vce);
        experience = vce.GetModifiedValue();
        this.PostNotification(ExperienceDidChange, experience);

        if (experience >= ExperienceForLevel(level + 1))
        {
            this.PostNotification(CanLevelUp, LevelForExperience(experience));
        }
    }

    public int ExperienceForLevel(int lvl)
    {
        return experienceChart[Mathf.Clamp(lvl - 1, 0, experienceChart.Length - 1)];
    }

    public int LevelForExperience(int exp)
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
        SetLevel(lvl);
        SetExperience(ExperienceForLevel(lvl));
    }

    public void IncreaseLevel()
    {
        SetLevel(level + 1);
    }

    public void DecreaseLevel()
    {
        SetLevel(level - 1);
    }

    public void SetLevel(int level)
    {
        int oldLevel = this.level;
        int newLevel = Mathf.Clamp(level, minLevel, maxLevel);
        if (oldLevel != newLevel)
        {
            BaseException exception = new BaseException(true);
            this.PostNotification(LevelWillChange, exception);
            if (exception.toggle)
            {
                this.level = newLevel;
                SetExperience(ExperienceForLevel(newLevel));
                this.PostNotification(LevelDidChange, new LevelChangeArgs(oldLevel, newLevel));
            }
        }
    }
    #endregion
}
