using UnityEngine;
using System.Collections.Generic;
using Handler = System.Action<System.Object, System.Object>;
using SenderTable = System.Collections.Generic.Dictionary<System.Object, System.Collections.Generic.List<System.Action<System.Object, System.Object>>>;

public class NotificationHandler
{
    #region Properties
    private Dictionary<string, SenderTable> table = new Dictionary<string, SenderTable>();
    private HashSet<List<Handler>> invoking = new HashSet<List<Handler>>();

    public static readonly NotificationHandler instance = new NotificationHandler();
    private NotificationHandler() { }
    #endregion

    #region Public
    public void AddListener(Handler handler, string notification)
    {
        AddListener(handler, notification, null);
    }

    public void AddListener(Handler handler, string notification, System.Object sender)
    {
        if (handler == null)
        {
            Debug.LogError("Can not add listener with a handler of null.");
            return;
        }

        if (string.IsNullOrEmpty(notification))
        {
            Debug.LogError("Can not add listener with empty string.");
            return;
        }

        if (!table.ContainsKey(notification))
        {
            table.Add(notification, new SenderTable());
        }

        SenderTable subTable = table[notification];

        System.Object key = sender ?? (this);

        if (!subTable.ContainsKey(key))
        {
            subTable.Add(key, new List<Handler>());
        }

        List<Handler> list = subTable[key];

        if (!list.Contains(handler))
        {
            if (invoking.Contains(list))
            {
                subTable[key] = list = new List<Handler>(list);
            }

            list.Add(handler);
        }
    }

    public void RemoveListener(Handler handler, string notification)
    {
        RemoveListener(handler, notification, null);
    }

    public void RemoveListener(Handler handler, string notification, System.Object sender)
    {
        if (handler == null)
        {
            Debug.LogError("Can not remove listener with a handler of null.");
            return;
        }

        if (string.IsNullOrEmpty(notification))
        {
            Debug.LogError("Can not remove listener with empty string.");
            return;
        }
        
        if (!table.ContainsKey(notification))
        {
            return;
        }

        SenderTable subTable = table[notification];
        System.Object key = sender ?? (this);

        if (!subTable.ContainsKey(key))
        {
            return;
        }

        List<Handler> list = subTable[key];
        int index = list.IndexOf(handler);
        if (index != -1)
        {
            if (invoking.Contains(list))
            {
                subTable[key] = list = new List<Handler>(list);
            }
            list.RemoveAt(index);
        }
    }

    public void Clean()
    {
        string[] notKeys = new string[table.Keys.Count];
        table.Keys.CopyTo(notKeys, 0);
        for (int i = notKeys.Length - 1; i >= 0; i--)
        {
            string notificationName = notKeys[i];
            SenderTable senderTable = table[notificationName];
            object[] senKeys = new object[senderTable.Keys.Count];
            senderTable.Keys.CopyTo(senKeys, 0);
            for (int j = senKeys.Length - 1; j >= 0; j--)
            {
                object sender = senKeys[j];
                List<Handler> handlers = senderTable[sender];
                if (handlers.Count == 0)
                    senderTable.Remove(sender);
            }
            if (senderTable.Count == 0)
                table.Remove(notificationName);
        }
    }

    public void PostNotification(string notification)
    {
        PostNotification(notification, null);
    }

    public void PostNotification(string notification, System.Object sender)
    {
        PostNotification(notification, sender, null);
    }

    public void PostNotification(string notification, System.Object sender, System.Object e)
    {
        if (string.IsNullOrEmpty(notification))
        {
            Debug.LogError("Can not post notification to event with no name");
            return;
        }

        if (!table.ContainsKey(notification))
        {
            return;
        }

        SenderTable subTable = table[notification];
        if (sender != null && subTable.ContainsKey(sender))
        {
            List<Handler> handlers = subTable[sender];
            invoking.Add(handlers);

            for (int i = 0; i < handlers.Count; i++)
            {
                handlers[i](sender, e);
            }

            invoking.Remove(handlers);
        }

        if (subTable.ContainsKey(this))
        {
            List<Handler> handlers = subTable[this];
            invoking.Add(handlers);

            for (int i = 0; i < handlers.Count; i++)
            {
                handlers[i](sender, e);
            }

            invoking.Remove(handlers);
        }
    }
    #endregion
}
