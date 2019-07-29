using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationPanel : MonoBehaviour
{
    public Text message;
    public Image speaker;
    public GameObject Arrow;
    public Panel panel;

    private void Start()
    {
        Vector3 pos = Arrow.transform.localPosition;
        Arrow.transform.localPosition = new Vector3(pos.x, pos.y + 5, pos.z);
        Tweener t = Arrow.transform.MoveToLocal(new Vector3(pos.x, pos.y - 5, pos.z), 0.5f);
        t.easingControl.loopType = EasingControl.LoopType.PingPong;
        t.easingControl.loopCount = -1;
    }

    public IEnumerator Display(SpeakerData data)
    {
        speaker.sprite = data.speaker;
        speaker.SetNativeSize();

        for (int i = 0; i < data.messages.Count; i++)
        {
            message.text = data.messages[i];
            Arrow.SetActive(data.messages.Count > i + 1);
            yield return null;
        }
    }
}
