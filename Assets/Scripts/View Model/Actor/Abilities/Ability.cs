using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public ActionType actionType;

    public AbilityArea abilityArea
    {
        get { return gameObject.GetComponent<AbilityArea>(); }
    }

    public AbilityRange abilityRange
    {
        get { return gameObject.GetComponent<AbilityRange>(); }
    }

    public AbilityEffectTarget[] abilityEffectTarget
    {
        get { return gameObject.GetComponentsInChildren<AbilityEffectTarget>(); }
    }

    public Accuracy accuracy
    {
        get { return gameObject.GetComponent<Accuracy>(); }
    }
}
