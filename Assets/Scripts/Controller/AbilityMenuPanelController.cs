using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityMenuPanelController : MonoBehaviour
{
    const string Show = "Show";
    const string Hide = "Hide";
    const string AbilityMenuItemPoolKey = "AbilityMenuItemPoolKey";
    const int MenuCount = 4;

    [SerializeField] GameObject menuItemPrefab;
    [SerializeField] Text titleLabel;
    [SerializeField] Panel panel;
    [SerializeField] GameObject canvas;
    List<AbilityMenuItem> menuEntries = new List<AbilityMenuItem>(MenuCount);
    public int selection { get; private set; }

    void Awake()
    {
        PoolDataController.AddKey(AbilityMenuItemPoolKey, menuItemPrefab, int.MaxValue, MenuCount);
    }

    void Start()
    {
        panel.SetPosition(Hide, false);
        canvas.gameObject.SetActive(true);
    }

    Tweener TogglePos(string pos)
    {
        Tweener t = panel.SetPosition(pos, true);
        t.easingControl.duration = 0.5f;
        t.easingControl.equation = EasingEquations.EaseOutQuad;
        return t;
    }

    bool SetSelection(int index)
    {
        if (menuEntries[index].isLocked)
        {
            return false;
        }

        if (selection >= 0 && selection < menuEntries.Count)
        {
            menuEntries[selection].isSelected = false;
        }

        selection = index;

        if (selection >= 0 && selection < menuEntries.Count)
        {
            menuEntries[selection].isSelected = true;
        }
        return true;
    }

    AbilityMenuItem Dequeue()
    {
        Poolable p = PoolDataController.Dequeue(AbilityMenuItemPoolKey);
        AbilityMenuItem item = p.GetComponent<AbilityMenuItem>();
        item.transform.SetParent(panel.transform);
        item.transform.localScale = Vector3.one;
        item.gameObject.SetActive(true);
        item.Reset();
        return item;
    }

    void Enqueue(AbilityMenuItem item)
    {
        Poolable p = GetComponent<Poolable>();
        PoolDataController.Enqueue(p);
    }

    void Clear()
    {
        for (int i = 0; i < menuEntries.Count; i++)
        {
            Enqueue(menuEntries[i]);
        }
        menuEntries.Clear();
    }
}
