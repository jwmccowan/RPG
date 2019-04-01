using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    int hp
    {
        get { return stats[StatTypes.HP]; }
        set { stats[StatTypes.HP] = value; }
    }

    int maxHP
    {
        get { return stats[StatTypes.Max_HP]; }
    }

    int negativeConstitution
    {
        get { return stats[StatTypes.Constitution] * -1; }
    }

    Stats stats;
    Level level;

    public void ChangeMaxHP(int delta)
    {
        stats[StatTypes.HP_Increases] += delta;
    }

    void Awake()
    {
        stats = GetComponent<Stats>();
        level = GetComponent<Level>();

    }

    void OnEnable()
    {
        this.AddListener(OnHpWillChange, Stats.WillChangeNotification(StatTypes.HP), stats);
        this.AddListener(OnMaxHpDidChange, Stats.DidChangeNotification(StatTypes.Max_HP), stats);
        this.AddListener(OnConstitutionBonusChanged, Stats.DidChangeNotification(StatTypes.Constitution_Bonus), stats);
        this.AddListener(OnClassLevelAdded, Level.ClassLevelAddedNotification, level);
        SetConBonusToHP();
    }

    void OnDisable()
    {
        this.RemoveListener(OnHpWillChange, Stats.WillChangeNotification(StatTypes.HP), stats);
        this.RemoveListener(OnMaxHpDidChange, Stats.DidChangeNotification(StatTypes.Max_HP), stats);
        this.RemoveListener(OnConstitutionBonusChanged, Stats.DidChangeNotification(StatTypes.Constitution_Bonus), stats);
        this.RemoveListener(OnClassLevelAdded, Level.ClassLevelAddedNotification, level);
        stats[StatTypes.Con_Bonus_To_HP] = 0;
    }

    void OnHpWillChange(object sender, object e)
    {
        Info<StatTypes, ValueChangeException> info = (Info<StatTypes, ValueChangeException>)e;
        ValueChangeException vce = info.arg1;
        vce.AddModifier(new ClampValueModifier(int.MaxValue, negativeConstitution, maxHP));
    }

    void OnMaxHpDidChange(object sender, object e)
    {
        Info<StatTypes, int> info = (Info<StatTypes, int>)e;
        int delta = maxHP - info.arg1;
        hp = Mathf.Clamp(hp + delta, negativeConstitution, maxHP);
    }

    void OnConstitutionBonusChanged(object sender, object e)
    {
        SetConBonusToHP();
    }

    void OnClassLevelAdded(object sender, object e)
    {
        SetConBonusToHP();
    }

    void SetConBonusToHP()
    {
        stats[StatTypes.Con_Bonus_To_HP] = level.classLevelCount * stats[StatTypes.Constitution_Bonus];
    }
}
