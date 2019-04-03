using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public const string StatusAddedNotification = "Status.StatusAddedNotification";
    public const string StatusRemovedNotification = "Status.StatusRemovedNotification";

    public U Add<T, U>()
        where T : StatusEffect
        where U : StatusCondition
    {
        T effect = GetComponentInChildren<T>();
        if (effect == null)
        {
            effect = gameObject.AddChildComponent<T>();
            this.PostNotification(StatusAddedNotification, effect);
        }

        return gameObject.AddChildComponent<U>();
    }

    public void Remove(StatusCondition target)
    {
        StatusEffect effect = GetComponentInChildren<StatusEffect>();
        target.transform.SetParent(null);
        Destroy(target.gameObject);

        StatusCondition condition = GetComponentInChildren<StatusCondition>();
        if (condition == null)
        {
            effect.transform.SetParent(null);
            Destroy(effect.gameObject);
            this.PostNotification(StatusRemovedNotification, effect);
        }
    }
}
