using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentTestScript : MonoBehaviour
{
    List<GameObject> inventory = new List<GameObject>();
    List<GameObject> combatants = new List<GameObject>();

    private void Start()
    {
        DataController.instance.Load(null);
        CreateItems();
        CreateCombatants();
        StartCoroutine(SimulateBattle());
    }

    private void OnEnable()
    {
        this.AddListener(OnEquippedItem, Equipment.equippedNotification);
        this.AddListener(OnUnequippedItem, Equipment.unequippedNotification);
    }

    private void OnDisable()
    {
        this.RemoveListener(OnEquippedItem, Equipment.equippedNotification);
        this.RemoveListener(OnUnequippedItem, Equipment.unequippedNotification);
    }

    void OnEquippedItem(object sender, object e)
    {
        Equipment eq = sender as Equipment;
        Equippable item = e as Equippable;
        inventory.Remove(item.gameObject);
        Debug.Log(string.Format("{0} equipped {1}.", eq.name, item.name));
    }

    void OnUnequippedItem(object sender, object e)
    {
        Equipment eq = sender as Equipment;
        Equippable item = e as Equippable;
        inventory.Add(item.gameObject);
        Debug.Log(string.Format("{0} un-equipped {1}.", eq.name, item.name));
    }

    GameObject CreateConsumableItem(string title, int amount)
    {
        GameObject item = new GameObject(title);
        HealthModifierFeature smf = item.AddComponent<HealthModifierFeature>();
        smf.value = amount;
        item.AddComponent<Consumable>();
        return item;
    }

    GameObject CreateEquippableItem(string title, StatTypes type, BonusTypes bonus, int amount, EquipSlots slots)
    {
        GameObject item = new GameObject(title);
        StatBonusFeature sbf = item.AddComponent<StatBonusFeature>();
        sbf.SetModifier(type, new StatModifierTotalAdd(amount, bonus));
        Equippable equip = item.AddComponent<Equippable>();
        equip.primarySlot = slots;
        return item;
    }

    GameObject CreateHero()
    {
        GameObject hero = CreateActor("Hero");
        hero.AddComponent<Equipment>();
        return hero;
    }

    GameObject CreateActor(string title)
    {
        GameObject actor = new GameObject(title);
        CharacterSheet sheet = actor.AddComponent<CharacterSheet>();
        sheet.stats.GetStat<Stat>(StatTypes.Ability_Score_Perception).statBaseValue = 16;
        sheet.stats.GetStat<Stat>(StatTypes.Ability_Score_Might).statBaseValue = 17;
        sheet.stats.GetStat<Stat>(StatTypes.Ability_Score_Constitution).statBaseValue = 16;
        sheet.stats.GetStat<Stat>(StatTypes.Ability_Score_Perception).statBaseValue = 16;
        sheet.stats.GetStat<StatRange>(StatTypes.Stat_Max_HP).SetCurrentValueToMax();
        return actor;
    }

    void CreateItems()
    {
        inventory.Add(CreateConsumableItem("Health Potion", 15));
        inventory.Add(CreateConsumableItem("Bomb", -25));
        inventory.Add(CreateEquippableItem("Sword", StatTypes.Ability_Score_Might, BonusTypes.Enhancement, 4 ,EquipSlots.Hand));
        inventory.Add(CreateEquippableItem("Broad Sword", StatTypes.Ability_Score_Might, BonusTypes.Enhancement, 8, (EquipSlots.Hand | EquipSlots.OffHand)));
        inventory.Add(CreateEquippableItem("Shield", StatTypes.Stat_Deflection, BonusTypes.Armor, 4, EquipSlots.OffHand));
    }

    void CreateCombatants()
    {
        combatants.Add(CreateHero());
        combatants.Add(CreateActor("Monster"));
    }

    IEnumerator SimulateBattle()
    {
        while (VictoryCheck() == false)
        {
            LogCombatants();
            HeroTurn();
            MonsterTurn();
            yield return new WaitForSeconds(1);
        }
        Debug.Log("Battle Complete.");
    }

    void HeroTurn()
    {
        int r = Random.Range(0, 2);
        switch (r)
        {
            case 0:
                Attack(combatants[0], combatants[1]);
                break;
            default:
                UseInventory();
                break;
        }
    }

    void MonsterTurn()
    {
        Attack(combatants[1], combatants[0]);
    }

    void Attack(GameObject attacker, GameObject defender)
    {
        CharacterSheet s1 = attacker.GetComponent<CharacterSheet>();
        CharacterSheet s2 = defender.GetComponent<CharacterSheet>();

        int roll = Random.Range(1, 101);
        int accuracy = s1.stats[StatTypes.Stat_Accuracy];
        int deflection = s2.stats[StatTypes.Stat_Deflection];
        int result = Random.Range(1, 101) + s1.stats[StatTypes.Stat_Accuracy] - s2.stats[StatTypes.Stat_Deflection];

        Debug.Log(string.Format("{0} attacks!  {1} + {2} - {3} = {4}", attacker.name, roll, accuracy, deflection, result));

        if (result >= 50)
        {
            int damageRoll = Random.Range(1, 9);
            float damageMod = s1.stats[StatTypes.Stat_Damage_Modifier];
            int damage = Mathf.FloorToInt(damageRoll * damageMod);
            s2.hp -= damage;
            Debug.Log(string.Format("Hit! {0} did {1} damage ({2} * {3})!", attacker.name, damage, damageRoll, damageMod));
        }
        else
        {
            Debug.Log("Miss :(");
        }
    }

    void UseInventory()
    {
        int r = Random.Range(0, inventory.Count);
        Debug.Log(r + " " + inventory.Count);

        GameObject item = inventory[r];
        if (item.GetComponent<Consumable>() != null)
        {
            ConsumeItem(item);
        }
        else
        {
            EquipItem(item);
        }
    }

    void ConsumeItem(GameObject item)
    {
        inventory.Remove(item);
        Debug.Log(item.name);
        StatModifierFeature smf = item.GetComponent<StatModifierFeature>();

        if (smf.value > 0)
        {
            item.GetComponent<Consumable>().Consume(combatants[0]);
            Debug.Log("Yummy potion..");
        }
        else
        {
            item.GetComponent<Consumable>().Consume(combatants[1]);
            Debug.Log("Take this bomb!");
        }
    }

    void EquipItem(GameObject item)
    {
        Debug.Log("Perhaps this will help..");
        Equippable toEquip = item.GetComponent<Equippable>();
        Equipment equipment = combatants[0].GetComponent<Equipment>();
        equipment.Equip(toEquip, toEquip.primarySlot);
    }

    bool VictoryCheck()
    {
        for (int i = 0; i < combatants.Count; i++)
        {
            StatRange health = combatants[i].GetComponent<CharacterSheet>().stats.GetStat<StatRange>(StatTypes.Stat_Max_HP);
            Debug.Log("Current HP: " + health.currentValue);
            if (health.currentValue == 0)
            {
                return true;
            }
        }
        return false;
    }

    void LogCombatants()
    {
        Debug.Log("============");
        for (int i = 0; i < 2; ++i)
            LogToConsole(combatants[i]);
        Debug.Log("============");
    }

    void LogToConsole(GameObject actor)
    {
        CharacterSheet sheet = actor.GetComponent<CharacterSheet>();
        string message = string.Format("Name:{0} HP:{1}/{2} Strength:{3} AC:{4}",
            actor.name,
            sheet.stats.GetStat<StatRange>(StatTypes.Stat_Max_HP).currentValue,
            sheet.stats[StatTypes.Stat_Max_HP],
            sheet.stats[StatTypes.Stat_Accuracy],
            sheet.stats[StatTypes.Stat_Deflection]);
        Debug.Log(message);
    }
}
