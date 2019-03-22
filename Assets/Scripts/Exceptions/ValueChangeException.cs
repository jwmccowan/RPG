using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueChangeException : BaseException
{
    #region fields
    public readonly int fromValue;
    public readonly int toValue;
    public int delta { get { return toValue - fromValue; } }
    List<ValueModifier> modifiers;
    #endregion

    #region Constructor
    public ValueChangeException(int fromValue, int toValue)
        : base(true)
    {
        this.fromValue = fromValue;
        this.toValue = toValue;
    }
    #endregion

    #region public
    public void AddModifier(ValueModifier m)
    {
        if (modifiers == null)
        {
            modifiers = new List<ValueModifier>();
        }
        modifiers.Add(m);
    }

    public int GetModifiedValue()
    {
        if (modifiers == null)
        {
            return toValue;
        }
        int retValue = toValue;

        modifiers.Sort(Compare);
        for (int i = 0; i < modifiers.Count; i++)
        {
            retValue = modifiers[i].Modify(fromValue, retValue);
        }
        return retValue;
    }
    #endregion

    #region private
    int Compare(ValueModifier a, ValueModifier b)
    {
        return a.sortOrder.CompareTo(b.sortOrder);
    }
    #endregion
}
