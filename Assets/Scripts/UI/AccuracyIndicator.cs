using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccuracyIndicator : MonoBehaviour
{
    const string ShowKey = "Show";
    const string HideKey = "Hide";

    [SerializeField] Canvas canvas;
    [SerializeField] Panel panel;
    [SerializeField] Image arrow;
    [SerializeField] Text label;
    Tweener transition;

    void Start()
    {
        panel.SetPosition(HideKey, false);
        canvas.gameObject.SetActive(false);
    }

    public void SetStats(int chance, int amount)
    {
        arrow.fillAmount = (chance / 100f);
        label.text = string.Format("{0}% {1} pt(s)", chance, amount);
    }

    public void Show()
    {
        canvas.gameObject.SetActive(true);
        SetPanelPosition(ShowKey);
    }

    public void Hide()
    {
        SetPanelPosition(HideKey);
        this.AddListener(OnHideCompleted, EasingControl.CompleteEvent, transition);
    }

    void OnHideCompleted(object sender, object e)
    {
        if (panel.CurrentPosition == panel[HideKey])
        {
            canvas.gameObject.SetActive(false);
            this.RemoveListener(OnHideCompleted, EasingControl.CompleteEvent, transition);
        }
    }

    void SetPanelPosition(string key)
    {
        if (transition != null && transition.IsPlaying)
        {
            transition.Stop();
        }

        transition = panel.SetPosition(key, true);
        transition.duration = 0.5f;
        transition.equation = EasingEquations.EaseOutQuad;
    }
}
