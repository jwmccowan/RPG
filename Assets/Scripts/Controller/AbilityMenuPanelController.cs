using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityMenuPanelController : MonoBehaviour
{
    const string ShowKey = "Show";
    const string HideKey = "Hide";
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
        panel.SetPosition(HideKey, false);
        canvas.gameObject.SetActive(true);
    }

    Tweener TogglePos(string pos)
    {
        Tweener t = panel.SetPosition(pos, true);
        t.duration = 0.5f;
        t.equation = EasingEquations.EaseOutQuad;
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

    public void Next()
    {
        for (int i = selection + 1; i < selection + menuEntries.Count; i++)
        {
            int index = i % menuEntries.Count;
            if (SetSelection(index))
            {
                break;
            }
        }
    }

    public void Previous()
    {
        for (int i = selection - 1 + menuEntries.Count; i > selection; i--)
        {
            int index = i % menuEntries.Count;
            if (SetSelection(index))
            {
                break;
            }
        }
    }

    public void Show(string title, List<string> options)
    {
        canvas.SetActive(true);
        Clear();
        titleLabel.text = title;
        for (int i = 0; i < options.Count; i++)
        {
            AbilityMenuItem item = Dequeue();
            item.labelText = options[i];
            menuEntries.Add(item);
        }
        SetSelection(0);
        TogglePos(ShowKey);
    }

    public void SetLocked(int index, bool value)
    {
        if (index < 0 || index >= menuEntries.Count)
        {
            return;
        }

        menuEntries[index].isLocked = value;
        if (value && selection == index)
        {
            Next();
        }
    }

    public void Hide()
    {
        Tweener t = TogglePos(HideKey);
        this.AddListener(OnHideCompleted, EasingControl.CompleteEvent, panel.Transition);
    }

    void OnHideCompleted(object sender, object e)
    {
        if (panel.CurrentPosition == panel[HideKey])
        {
            Clear();
            canvas.SetActive(false);
            this.RemoveListener(OnHideCompleted, EasingControl.CompleteEvent, panel.Transition);
        }
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
        Poolable p = item.GetComponent<Poolable>();
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
