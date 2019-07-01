using UnityEngine;

public class Equippable : MonoBehaviour
{
    public EquipSlots primarySlot;
    public EquipSlots secondarySlot;
    public EquipSlots slots;
    bool isEquipped;

    public void OnEquip()
    {
        if (isEquipped)
        {
            return;
        }
        isEquipped = true;

        Feature[] features = GetComponentsInChildren<Feature>();
        for (int i = 0; i < features.Length; i++)
        {
            features[i].Activate(gameObject);
        }
    }

    public void OnUnEquip()
    {
        if (!isEquipped)
        {
            return;
        }
        isEquipped = false;

        Feature[] features = GetComponentsInChildren<Feature>();
        for (int i = 0; i < features.Length; i++)
        {
            features[i].Deactivate();
        }
    }
}
