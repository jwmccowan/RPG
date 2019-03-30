using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassLevel
{
    public ClassType classType { get; private set; }
    int level;
    float growthRate;

    int willSave;
    int reflexSave;
    int fortitudeSave;

    int bab;

    Roll hpRoll;

    bool isApplied;

    public ClassLevel(ClassType classType, int level)
    {
        this.classType = classType;
        this.level = level;
        growthRate = DataController.classes[classType].growth_rate / 100f;
        CalculateClassLevelStats();
        //TODO: Get a list of abilities learned at this level
    }

    public void Apply(Stats stats, bool firstLevel)
    {
        if (!isApplied)
        {
            isApplied = true;
            stats[StatTypes.Will] += willSave;
            stats[StatTypes.Reflex] += reflexSave;
            stats[StatTypes.Fortitude] += fortitudeSave;
            stats[StatTypes.BAB] += bab;
            hpRoll.NewRoll();
            if (firstLevel)
            {
                hpRoll.LoadDie(int.MaxValue);
            }
            stats[StatTypes.HP_Increases] += hpRoll.value;
            //TODO: attach abilities to character
            stats[StatTypes.Movement] += 2; //this is fake, done by race and feats
        }
    }

    public void Remove(Stats stats)
    {
        if (isApplied)
        {
            isApplied = false;
            stats[StatTypes.Will] -= willSave;
            stats[StatTypes.Reflex] -= reflexSave;
            stats[StatTypes.Fortitude] -= fortitudeSave;
            stats[StatTypes.BAB] -= bab;
            stats[StatTypes.HP_Increases] -= hpRoll.value;
            //TODO: attach abilities to character
            stats[StatTypes.Movement] -= 2; // fake
        }
    }

    void CalculateClassLevelStats()
    {
        willSave = CalculateSaveDiff((GrowthRate)DataController.classes[classType].will_growth_rate);
        reflexSave = CalculateSaveDiff((GrowthRate)DataController.classes[classType].reflex_growth_rate);
        fortitudeSave = CalculateSaveDiff((GrowthRate)DataController.classes[classType].fortitude_growth_rate);
        bab = CalculateBABDiff();
        hpRoll = new Roll(DataController.classes[classType].hit_die);
    }

    int CalculateSaveDiff(GrowthRate rate)
    {
        return CalculateSave(rate, level) - CalculateSave(rate, level - 1);
    }

    static int CalculateSave(GrowthRate rate, int lev)
    {
        if (lev > 0)
        {
            switch (rate)
            {
                case GrowthRate.Good:
                    return Mathf.FloorToInt(2 + (lev / 2f));
                case GrowthRate.Medium:
                    return Mathf.FloorToInt(1 + (lev / 2.5f));
                case GrowthRate.Poor:
                    return Mathf.FloorToInt(lev / 3f);
            }
        }
        return 0;
    }

    int CalculateBABDiff()
    {
        return CalculateBAB(growthRate, level) - CalculateBAB(growthRate, level - 1);
    }

    static int CalculateBAB(float growth, int lev)
    {
        if (lev > 0)
        {
            return Mathf.FloorToInt(growth * lev);
        }
        return 0;
    }

    enum GrowthRate
    {
        Poor = 1,
        Medium = 2,
        Good = 3
    }
}
