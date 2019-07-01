using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Roll damageDie;
    [SerializeField] DamageType damageType;
    [SerializeField] int range;

    public int RollAttack()
    {
        CharacterSheet sheet = GetComponentInParent<CharacterSheet>();
        int retValue = 1;
        return retValue;
    }
}
