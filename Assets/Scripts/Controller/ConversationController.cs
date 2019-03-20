using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationController : MonoBehaviour
{
    [SerializeField] ConversationPanel leftPanel;
    [SerializeField] ConversationPanel rightPanel;
    Canvas canvas;

    IEnumerator conversation;
    Tweener transition;

    const string ShowTop = "ShowTop";
    const string ShowBottom = "ShowBottom";
    const string HideTop = "HideTop";
    const string HideBottom = "HideBottom";

    public const string ConversationComplete = "ConversationController.ConversationComplete";

    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        if (leftPanel.panel.CurrentPosition == null)
        {
            leftPanel.panel.SetPosition(HideBottom, false);
        }
        if (rightPanel.panel.CurrentPosition == null)
        {
            rightPanel.panel.SetPosition(HideBottom, false);
        }
        canvas.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        this.RemoveListener(OnCompletedEvent, EasingControl.CompletedEvent);
    }

    public void Show(ConversationData data)
    {
        canvas.gameObject.SetActive(true);
        conversation = Sequence(data);
        conversation.MoveNext();
    }

    public void Next()
    {
        if (conversation == null || transition != null)
        {
            return;
        }

        conversation.MoveNext();
    }

    IEnumerator Sequence(ConversationData data)
    {
        for (int i = 0; i < data.conversationData.Count; i++)
        {
            SpeakerData sd = data.conversationData[i];

            ConversationPanel currentPanel = (sd.anchor == TextAnchor.UpperLeft || sd.anchor == TextAnchor.MiddleLeft || sd.anchor == TextAnchor.LowerLeft) ? leftPanel : rightPanel;
            IEnumerator presenter = currentPanel.Display(sd);
            presenter.MoveNext();

            string show, hide;
            if (sd.anchor == TextAnchor.UpperLeft || sd.anchor == TextAnchor.UpperCenter || sd.anchor == TextAnchor.UpperRight)
            {
                show = ShowTop;
                hide = HideTop;
            }
            else
            {
                show = ShowBottom;
                hide = HideBottom;
            }

            currentPanel.panel.SetPosition(hide, false);
            MovePanel(currentPanel, show);

            yield return null;
            while (presenter.MoveNext())
            {
                yield return null;
            }
            
            MovePanel(currentPanel, hide);
            this.AddListener(OnCompletedEvent, EasingControl.CompletedEvent);
            yield return null;
        }

        canvas.gameObject.SetActive(false);
        this.PostNotification(ConversationComplete);
    }

    void MovePanel(ConversationPanel obj, string pos)
    {
        transition = obj.panel.SetPosition(pos, true);
        transition.easingControl.duration = 0.5f;
        transition.easingControl.equation = EasingEquations.EaseOutQuad;
    }

    void OnCompletedEvent(object sender, object e)
    {
        if ((object)sender == transition.easingControl)
        {
            conversation.MoveNext();
            this.RemoveListener(OnCompletedEvent, EasingControl.CompletedEvent);
        }
    }
}
