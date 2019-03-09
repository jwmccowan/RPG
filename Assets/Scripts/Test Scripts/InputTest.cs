using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.AddListener(OnMove, "InputController.MoveEventNotification");
        this.AddListener(OnFire, "InputController.FireEventNotification");
    }

    void OnMove(object sender, object e)
    {
        Point p = (Point)e;
        Debug.Log(string.Format("Move Event Received: {0}", e.ToString()));
    }

    void OnFire(object sender, object e)
    {
        int p = (int)e;
        Debug.Log(string.Format("Fire Event Received: {0}", e.ToString()));
    }
}
