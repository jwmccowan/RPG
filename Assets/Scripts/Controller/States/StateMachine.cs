using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public virtual State currentState
    {
        get { return _currentState; }
        set { Transition(value); }
    }
    protected State _currentState;
    protected bool inTransition;

    public virtual void ChangeState<T>()
        where T : State
    {
        currentState = GetState<T>();
    }

    public virtual T GetState<T>() 
        where T : State
    {
        T target = GetComponent<T>();
        if (target == null)
        {
            target = gameObject.AddComponent<T>();
        }
        return target;
    }

    protected virtual void Transition(State s)
    {
        if (inTransition == true || _currentState == s)
        {
            return;
        }

        inTransition = true;

        if (currentState != null)
        {
            currentState.Exit();
        }

        _currentState = s;

        if (_currentState != null)
        {
            currentState.Enter();
        }

        inTransition = false;
    }
}
