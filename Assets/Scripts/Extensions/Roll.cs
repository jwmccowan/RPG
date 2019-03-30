using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll
{
    public int numDice { get; private set; }
    public int numSides { get; private set; }
    public int bonus { get; private set; }
    public int value { get; private set; }
    Random random;

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
        numDice = nd;
        numSides = ns;
        bonus = b;
        random = new Random();
        NewRoll();
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
