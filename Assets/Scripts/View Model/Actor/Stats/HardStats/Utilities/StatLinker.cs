using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatLinker
{
    private Stat _stat;

    public const string LinkedValueDidChange = "StatLinker.LinkedValueDidChange";

    public Stat stat
    {
        get { return _stat; }
    }

    public abstract float value { get; }

    public StatLinker(Stat stat)
    {
        _stat = stat;
        this.AddListener(OnValueChanged, StatCollection.StatValueDidChangeNotification, stat);
    }

    private void OnValueChanged(object sender, object e)
    {
        PostLinkedChangeEvent();
    }

    private void PostLinkedChangeEvent()
    {
        this.PostNotification(LinkedValueDidChange, this);
    }
}
