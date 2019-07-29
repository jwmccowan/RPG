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
            return _currentValue;
        }

        set
        {
            float newValue = Mathf.Clamp(value, minValue, statValue);
            //TODO: the vce object might change a bit here, let's not send anything for now
            if (!currentValue.Equals(newValue))
            {
                //TODO: vce should use float now
                //ValueChangeException vce = new ValueChangeException(currentValue, value);
                owner.PostNotification(StatCollection.CurrentValueWillChange(statType), this);
                _currentValue = newValue;
                owner.PostNotification(StatCollection.CurrentValueDidChange(statType), this);
            }
        }
    }

    public StatRange()
    {
        _currentValue = 0;
        minValue = 0;
        this.AddListener(OnValueChanged, StatCollection.ValueDidChange(StatTypes.Stat_Max_HP), owner);
    }

    public void SetCurrentValueToMax()
    {
        _currentValue = statValue;
    }

    private void OnValueChanged(object sender, object e)
    {
        StatValueChangeArgs vca = e as StatValueChangeArgs;
        float delta = vca.newValue - vca.oldValue;
        if (delta > 0)
        {
            currentValue += delta;
        }
    }
}
