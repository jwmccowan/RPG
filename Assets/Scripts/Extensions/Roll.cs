using UnityEngine;

[System.Serializable]
public class Roll
{
    static Random random;

    public int numDice { get; private set; }
    public int numSides { get; private set; }
    public int bonus { get; private set; }
    public int value { get; private set; }

    public Roll(int numSides)
    {
        Init(1, numSides, 0);
    }

    public Roll(int numDice, int numSides)
    {
        Init(numDice, numSides, 0);
    }

    public Roll(int numDice, int numSides, int bonus)
    {
        Init(numDice, numSides, bonus);
    }

    void Init(int nd, int ns, int b)
    {
        if (random == null)
        {
            random = new Random();
        }
        numDice = nd;
        numSides = ns;
        bonus = b;
    }

    public int NewRoll()
    {
        value = NewRoll(numDice, numSides, bonus);
        return value;
    }

    public static int NewRoll(int numDice, int numSides, int bonus)
    {
        int result = bonus;
        for (int i = 0; i < numDice; i++)
        {
            result += Random.Range(1, numSides + 1);
        }
        return result;
    }

    public void LoadDie(int loadedRoll)
    {
        value = Mathf.Clamp(loadedRoll, numDice + bonus, (numDice * numSides) + bonus);
    }
}
