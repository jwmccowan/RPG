using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatRange : StatAttribute
{
    private float _currentValue;
    private float minValue;
    
    public float currentValue
    {
        get
        {
            _currentValue = Mathf.Clamp(_currentValue, minValue, statValue);
            return _currentValue;
        }

        set
        {
            ValueChangeException vce = new ValueChangeException((int) _currentValue, (int) value);
            //TODO: the vce object might change a bit here, let's not send anything for now
            this.PostNotification(StatCollection.CurrentValueWillChange(statType));
            _currentValue = value;
            this.PostNotification(StatCollection.CurrentValueDidChange(statType), this);
        }
    }

    public StatRange()
    {
        _currentValue = 0;
        minValue = 0;
    }

    public void SetCurrentValueToMax()
    {
        _currentValue = statValue;
    }
}
