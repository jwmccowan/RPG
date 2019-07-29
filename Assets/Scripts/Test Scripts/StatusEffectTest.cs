using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectTest : MonoBehaviour
{
    Unit cursedUnit;
    Equippable cursedItem;
    int step;

    void OnEnable()
    {
        this.AddListener(OnTurnBegan, InitiativeController.TurnBeganNotification);
    }

    void OnDisable()
    {
        this.RemoveListener(OnTurnBegan, InitiativeController.TurnBeganNotification);
    }

    void OnTurnBegan(object sender, object e)
    {
        BaseException exc = e as BaseException;
        if (exc.toggle == false)
        {
            return;
        }

        Unit target = sender as Unit;
        switch (step)
        {
            case 0:
                EquipCursedItem(target);
                break;
            case 1:
                Add<BlessStatusEffect>(target, 10);
                break;
            default:
                UnEquipCursedItem(target);
                break;
        }
        step++;
    }

    void Add<T>(Unit target, int duration)
        where T : StatusEffect
    {
        DurationStatusCondition condition = target.GetComponent<Status>().Add<T, DurationStatusCondition>();
        condition.duration = duration;
    }

    void EquipCursedItem(Unit target)
    {
        Debug.Log(string.Format("Adding cursed sword to {0}.", target.gameObject.name));

        cursedUnit = target;

        GameObject obj = new GameObject("Cursed Sword");
        obj.AddComponent<AddPoisonStatusFeature>();
        cursedItem = obj.AddComponent<Equippable>();
        cursedItem.primarySlot = EquipSlots.Hand;

        Equipment equipment = target.GetComponent<Equipment>();
        equipment.Equip(cursedItem, cursedItem.primarySlot);
    }

    void UnEquipCursedItem(Unit target)
    {
        if (target != cursedUnit || step < 10)
        {
            return;
        }
        Equipment equipment = target.GetComponent<Equipment>();
        equipment.Unequip(cursedItem);

        Destroy(cursedItem.gameObject);

        Destroy(this);
    }
}
