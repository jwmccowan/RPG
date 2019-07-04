using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] Stat[] data = new Stat[(int)StatTypes.Count];

    public int this[StatTypes s]
    {
        get { return Mathf.FloorToInt(data[(int)s].statValue); }
        set { SetValue(s, value, true); }
    }

    static Dictionary<StatTypes, string> _willChangeNotifications = new Dictionary<StatTypes, string>();
    static Dictionary<StatTypes, string> _didChangeNotifications = new Dictionary<StatTypes, string>();

    public static string WillChangeNotification(StatTypes type)
    {
        if (!_willChangeNotifications.ContainsKey(type))
        {
            _willChangeNotifications.Add(type, string.Format("Stats.{0}WillChange", type.ToString()));
        }
        return _willChangeNotifications[type];
    }

    public static string DidChangeNotification(StatTypes type)
    {
        if (!_didChangeNotifications.ContainsKey(type))
        {
            _didChangeNotifications.Add(type, string.Format("Stats.{0}DidChange", type.ToString()));
        }
        return _didChangeNotifications[type];
    }

    public void SetValue(StatTypes s, int value, bool allowExceptions)
    {
        int oldValue = Mathf.FloorToInt(data[(int) s].statValue);

        if (oldValue == value)
        {
            return;
        }

        if (allowExceptions)
        {
            ValueChangeException exc = new ValueChangeException(oldValue, value);
            this.PostNotification(WillChangeNotification(s), new Info<StatTypes, ValueChangeException>(s, exc));

            value = exc.GetModifiedValue();

            if (exc.toggle == false || value == oldValue)
            {
                return;
            }
        }

        data[(int) s].statBaseValue = value;
        this.PostNotification(DidChangeNotification(s), new Info<StatTypes, int>(s, oldValue));
    }


}
