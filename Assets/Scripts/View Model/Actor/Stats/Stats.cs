﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    int[] data = new int[(int)StatTypes.Count];

    public int this[StatTypes s]
    {
        get { return data[(int)s]; }
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
        int oldValue = this[s];

        if (oldValue == value)
        {
            return;
        }

        if (allowExceptions)
        {
            ValueChangeException exc = new ValueChangeException(oldValue, value);
            this.PostNotification(WillChangeNotification(s), exc);

            value = exc.GetModifiedValue();

            if (exc.toggle == false || value == oldValue)
            {
                return;
            }
        }

        data[(int)s] = value;
        this.PostNotification(DidChangeNotification(s), oldValue);
    }
}
