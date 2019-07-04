[System.Serializable]
public struct Bonus
{
    public readonly StatTypes statType;
    public readonly BonusTypes bonusType;
    public readonly int value;

    public Bonus(StatTypes statType, BonusTypes bonusType, int value)
    {
        this.statType = statType;
        this.bonusType = bonusType;
        this.value = value;
    }

    public static bool operator ==(Bonus a, Bonus b)
    {
        return a.statType == b.statType && a.bonusType == b.bonusType && a.value == b.value;
    }

    public static bool operator !=(Bonus a, Bonus b)
    {
        return !(a == b);
    }

    public override bool Equals(object obj)
    {
        if (obj is Bonus b)
        {
            return statType == b.statType && bonusType == b.bonusType && value == b.value;
        }
        return false;
    }

    public bool Equals(Bonus b)
    {
        return statType == b.statType && bonusType == b.bonusType && value == b.value;
    }

    public override int GetHashCode()
    {
        return unchecked (((int)statType ^ (int)bonusType) ^ value);
    }

    public override string ToString()
    {
        return string.Format("+{0} {1} bonus to {2}", value, bonusType.ToString(), statType.ToString());
    }
}
