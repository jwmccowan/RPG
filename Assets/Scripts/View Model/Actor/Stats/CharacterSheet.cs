using UnityEngine;
using System.Collections.Generic;

public class CharacterSheet : MonoBehaviour
{
    #region fields
    public StatCollection stats;
    public Level level;

    public int hp
    {
        get
        {
            return Mathf.FloorToInt(stats.GetStat<StatRange>(StatTypes.Stat_Max_HP).currentValue);            
        }
        set
        {
            stats.GetStat<StatRange>(StatTypes.Stat_Max_HP).currentValue = value;
        }
    }
    #endregion

    #region MonoBehaviour
    void OnEnable()
    {
    }

    void Awake()
    {
        stats = gameObject.AddComponent<DefaultStats>();
        level = gameObject.AddComponent<Level>();
        level.Init(1);

        this.AddListener(OnCanLevelUp, Level.CanLevelUp, level);
        this.AddListener(OnLevelUp, Level.LevelDidChange, level);
    }
    #endregion

    #region events
    void OnCanLevelUp(object sender, object e)
    {
        int i = (int)e;
        level.SetLevel(i);
    }
    void OnLevelUp(object sender, object e)
    {
        LevelChangeArgs args = e as LevelChangeArgs;
        stats.ScaleStatCollection(args.newLevel);
    }
    #endregion

    #region public
    #endregion

    #region private
    #endregion
}
