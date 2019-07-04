using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonStatusEffect : StatusEffect
{
    Unit unit;
    public Roll roll = new Roll(4);

    void OnEnable()
    {
        unit = GetComponentInParent<Unit>();
        this.AddListener(OnTurnBegan, InitiativeController.TurnBeganNotification, unit);
    }

    private void OnDisable()
    {
        this.RemoveListener(OnTurnBegan, InitiativeController.TurnBeganNotification, unit);
    }

    void OnTurnBegan(object sender, object e)
    {
        CharacterSheet sheet = GetComponentInParent<CharacterSheet>();
        int damage = roll.NewRoll();
        Debug.Log(string.Format("Doing {0} points of poison damage", damage));
        //sheet.stats.SetValue(StatTypes.HP, sheet.stats[StatTypes.HP] - damage, false);
    }
}
