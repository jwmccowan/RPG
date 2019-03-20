using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityMenuItem : MonoBehaviour
{
    [SerializeField] Image bulletSprite;
    [SerializeField] Sprite selectedBulletSprite;
    [SerializeField] Sprite normalBulletSprite;
    [SerializeField] Sprite lockedBulletSprite;
    [SerializeField] Text label;
    Outline outline;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    public string labelText
    {
        get { return label.text; }
        set { label.text = value; }
    }

    public bool isLocked
    {
        get { return (state & States.Locked) != States.None; }
        set
        {
            if (value)
            {
                state |= States.Locked;
            }
            else
            {
                state &= ~States.Locked;
            }
        }
    }

    public bool isSelected
    {
        get { return (state & States.Selected) != States.None; }
        set
        {
            if (value)
            {
                state |= States.Selected;
            }
            else
            {
                state &= ~States.Selected;
            }
        }
    }

    States state
    {
        get { return _state; }
        set
        {
            if (_state != value)
            {
                _state = value;
                if (isLocked)
                {
                    bulletSprite.sprite = lockedBulletSprite;
                    label.color = Color.gray;
                    outline.effectColor = new Color32(20, 36, 44, 255);
                }
                else if (isSelected)
                {
                    bulletSprite.sprite = selectedBulletSprite;
                    label.color = new Color32(246, 202, 50, 255);
                    outline.effectColor = new Color32(20, 36, 44, 255);
                }
                else
                {
                    bulletSprite.sprite = normalBulletSprite;
                    label.color = Color.white;
                    outline.effectColor = new Color32(20, 36, 44, 255);
                }
            }
        }
    }
    States _state;

    public void Reset()
    {
        state = States.None;
    }

    [System.Flags]
    enum States
    {
        None = 0,
        Selected = 1 << 0,
        Locked = 1 << 1
    }
}
