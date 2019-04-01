using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatPanelController : MonoBehaviour
{
    #region constants
    const string hideKey = "Hide";
    const string showKey = "Show";
    #endregion

    #region fields
    [SerializeField] StatPanel primaryPanel;
    [SerializeField] StatPanel secondaryPanel;

    Tweener primaryTransition;
    Tweener secondaryTransition;
    #endregion

    #region Monobehaviour
    void Start()
    {
        if (primaryPanel.panel.CurrentPosition == null)
        {
            primaryPanel.panel.SetPosition(hideKey, false);
        }
        if (secondaryPanel.panel.CurrentPosition == null)
        {
            secondaryPanel.panel.SetPosition(hideKey, false);
        }
    }
    #endregion

    #region public
    public void ShowPrimary(GameObject obj)
    {
        primaryPanel.Display(obj);
        MovePanel(primaryPanel, showKey, ref primaryTransition);
    }

    public void HidePrimary()
    {
        MovePanel(primaryPanel, hideKey, ref primaryTransition);
    }

    public void ShowSecondary(GameObject obj)
    {
        secondaryPanel.Display(obj);
        MovePanel(secondaryPanel, showKey, ref secondaryTransition);
    }

    public void HideSecondary()
    {
        MovePanel(secondaryPanel, showKey, ref secondaryTransition);
    }
    #endregion

    #region private
    void MovePanel(StatPanel panel, string key, ref Tweener t)
    {
        Panel.Position target = panel.panel[key];
        if (panel.panel.CurrentPosition != target)
        {
            if (t != null && t.easingControl != null)
            {
                t.easingControl.Stop();
            }
            t = panel.panel.SetPosition(key, true);
            t.easingControl.duration = 0.5f;
            t.easingControl.equation = EasingEquations.EaseOutQuad;
        }
    }
    #endregion
}
