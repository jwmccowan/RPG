using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Feature : MonoBehaviour
{
    #region fields
    protected GameObject target { get; private set; }
    #endregion

    #region public
    public void Activate(GameObject target)
    {
        if (this.target == null)
        {
            this.target = target;
            OnApply();
        }
    }

    public void Deactivate()
    {
        if (target != null)
        {
            OnRemove();
            target = null;
        }
    }

    public void Apply(GameObject target)
    {
        this.target = target;
        OnApply();
        this.target = null;
    }
    #endregion

    #region private
    protected abstract void OnApply();
    protected virtual void OnRemove() { }
    #endregion
}
