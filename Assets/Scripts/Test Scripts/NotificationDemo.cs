using UnityEngine;
using System.Collections;
namespace Notifications.Test1
{
    public class NotificationDemo : MonoBehaviour
    {
        public const string notification = "Demo.Notification";
        public int listenerCount = 250;
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                Test();
            }
        }
        void Test()
        {
            Listener[] listeners = new Listener[listenerCount];
            for (int i = 0; i < listenerCount; ++i)
            {
                listeners[i] = new Listener();
                listeners[i].Enable();
            }

            this.PostNotification(notification);

            for (int i = 0; i < listeners.Length; ++i)
                listeners[i].Disable();
            NotificationHandler.instance.Clean();
        }
    }

    public class Listener
    {
        public const string Clear = "Listener.Clear";

        public void Enable()
        {
            this.AddListener(OnTest, NotificationDemo.notification);
            this.AddListener(OnClear, Listener.Clear);
        }

        public void Disable()
        {
            this.RemoveListener(OnTest, NotificationDemo.notification);
            this.RemoveListener(OnClear, Listener.Clear);
        }

        void OnTest(object sender, object info)
        {
            Debug.Log("Got the message!");
            this.PostNotification(Listener.Clear);
        }

        void OnClear(object sender, object info)
        {
            this.RemoveListener(OnTest, NotificationDemo.notification);
        }
    }
}