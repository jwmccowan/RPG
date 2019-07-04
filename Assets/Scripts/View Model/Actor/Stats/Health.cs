using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int hp;
    //public int hp
    //{
        //get { return stats[StatTypes.HP]; }
        //set { stats[StatTypes.HP] = value; }
    //}

    public int maxMaxHP
    {
        get { return stats[StatTypes.Stat_Max_HP]; }
    }

    public int minHP = 0;

    Stats stats;
    Level level;

    void Awake()
    {
        stats = GetComponent<Stats>();
        level = GetComponent<Level>();
    }

    void OnEnable()
    {/*
        this.AddListener(OnHpWillChange, Stats.WillChangeNotification(StatTypes.HP), stats);
        this.AddListener(OnMaxHpDidChange, Stats.DidChangeNotification(StatTypes.Max_HP), stats);
        this.AddListener(OnConstitutionChanged, Stats.DidChangeNotification(StatTypes.Ability_Score_Constitution), stats);
        this.AddListener(OnClassLevelAdded, Level.ClassLevelAddedNotification, level);
        SetConBonusToHP();
        */
    }

    void OnDisable()
    {
        /*
        this.RemoveListener(OnHpWillChange, Stats.WillChangeNotification(StatTypes.HP), stats);
        this.RemoveListener(OnMaxHpDidChange, Stats.DidChangeNotification(StatTypes.Max_HP), stats);
        this.RemoveListener(OnConstitutionChanged, Stats.DidChangeNotification(StatTypes.Ability_Score_Constitution), stats);
        this.RemoveListener(OnClassLevelAdded, Level.ClassLevelAddedNotification, level);
        stats[StatTypes.Stat_Max_HP] = 0;
        */
    }

    void OnHpWillChange(object sender, object e)
    {
        Info<StatTypes, ValueChangeException> info = (Info<StatTypes, ValueChangeException>)e;
        ValueChangeException vce = info.arg1;
        vce.AddModifier(new ClampValueModifier(int.MaxValue, minHP, maxMaxHP));
    }

    void OnMaxHpDidChange(object sender, object e)
    {
        Info<StatTypes, int> info = (Info<StatTypes, int>)e;
        int delta = maxMaxHP - info.arg1;
        hp = Mathf.Clamp(hp + delta, minHP, maxMaxHP);
    }

    void OnConstitutionChanged(object sender, object e)
    {
        SetConBonusToHP();
    }
    
    // TOREMOVE: will this be class level?  Does this matter?
    void OnClassLevelAdded(object sender, object e)
    {
        SetConBonusToHP();
    }

    void SetConBonusToHP()
    {
        // TODO: This is the formula for how Constitution affects HP
        stats[StatTypes.Stat_Max_HP] = stats[StatTypes.Ability_Score_Constitution] * stats[StatTypes.Level];
    }
}
