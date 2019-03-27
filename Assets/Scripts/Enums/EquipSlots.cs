using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum EquipSlots
{
    None = 0,
    Belts = 1 << 0,
    Body = 1 << 1,
    Chest = 1 << 2,
    Eyes = 1 << 3,
    Feet = 1 << 4,
    Hand = 1 << 5,
    Head = 1 << 6,
    Headband = 1 << 7,
    Neck = 1 << 8,
    RingLeft = 1 << 9,
    RingRight = 1 << 10,
    Shield = 1 << 11,
    Shoulders = 1 << 12,
    Wrist = 1 << 13,
    Armor = 1 << 14,
    OffHand = 1 << 15
}
