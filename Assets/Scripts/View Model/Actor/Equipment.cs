using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    #region constants
    public const string equippedNotification = "Equipment.EquippedNotification";
    public const string unequippedNotification = "Equipment.UnequippedNotification";
    #endregion

    #region fields
    public IList <Equippable> items { get { return itemList.AsReadOnly(); } }
    public List<Equippable> itemList = new List<Equippable>();
    #endregion

    #region public
    public void Equip(Equippable item, EquipSlots slots)
    {
        Unequip(slots);

        itemList.Add(item);
        item.transform.SetParent(transform);
        item.slots = slots;
        item.OnEquip();

        this.PostNotification(equippedNotification, item);
    }

    public void Unequip(Equippable item)
    {
        item.OnUnEquip();
        item.slots = EquipSlots.None;
        item.transform.SetParent(transform);
        itemList.Remove(item);

        this.PostNotification(unequippedNotification, item);
    }

    public void Unequip(EquipSlots slots)
    {
        for (int i = itemList.Count - 1; i >= 0; i--)
        {
            if ((itemList[i].slots & slots) != EquipSlots.None)
            {
                Unequip(itemList[i]);
            }
        }
    }
    #endregion
}
