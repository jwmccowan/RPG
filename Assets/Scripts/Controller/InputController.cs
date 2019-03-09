using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public const string moveEventNotification = "InputController.MoveEventNotification";
    public const string fireEventNotification = "InputController.FireEventNotification";

    Repeater horizontal = new Repeater("Horizontal");
    Repeater vertical = new Repeater("Vertical");

    public void Update()
    {
        int x = horizontal.Update();
        int y = vertical.Update();

        if (x != 0 || y != 0)
        {
            this.PostNotification(moveEventNotification, new Point(x, y));
        }

        string[] fireEventNames = new string[] { "Fire1", "Fire2", "Fire3" };

        for (int i = 0; i < fireEventNames.Length; i++)
        {
            if(Input.GetButtonUp(fireEventNames[i]))
            {
                this.PostNotification(fireEventNotification, i);
            }
        }
    }


    class Repeater
    {
        const float threshold = 0.5f;
        const float rate = 0.25f;
        string axisName;
        float next;
        bool hold;

        public Repeater(string axisName)
        {
            this.axisName = axisName;
            next = 0.0f;
            hold = false;
        }

        public int Update()
        {
            int retValue = 0;
            int axisValue = Mathf.RoundToInt(Input.GetAxisRaw(axisName));

            if (axisValue != 0) {
                if (Time.time > next)
                {
                    retValue = axisValue;
                    next = Time.time + (hold ? rate : threshold);
                    hold = true;
                }
            }
            else
            {
                hold = false;
                next = 0.0f;
            }

            return retValue;
        }
    }
}
