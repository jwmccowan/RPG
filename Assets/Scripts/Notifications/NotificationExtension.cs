using Handler = System.Action<System.Object, System.Object>;

public static class NotificationExtension
{
    public static void PostNotification(this object obj, string notification)
    {
        NotificationHandler.instance.PostNotification(notification, obj);
    }

    public static void PostNotification(this object obj, string notification, object e)
    {
        NotificationHandler.instance.PostNotification(notification, obj, e);
    }

    public static void AddListener(this object obj, Handler handler, string notification)
    {
        NotificationHandler.instance.AddListener(handler, notification);
    }

    public static void AddListener(this object obj, Handler handler, string notification, object sender)
    {
        NotificationHandler.instance.AddListener(handler, notification, sender);
    }

    public static void RemoveListener(this object obj, Handler handler, string notification)
    {
        NotificationHandler.instance.RemoveListener(handler, notification);
    }

    public static void RemoveListener(this object obj, Handler handler, string notification, object sender)
    {
        NotificationHandler.instance.RemoveListener(handler, notification, sender);
    }
}
