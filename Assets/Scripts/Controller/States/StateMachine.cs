using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public virtual State CurrentState
    {
        get { return _currentState; }
        set { Transition(value); }
    }
    protected State _currentState;
    protected bool inTransition;

    public virtual void ChangeState<T>()
        where T : State
    {
        CurrentState = GetState<T>();
    }

    public virtual T GetState<T>() 
        where T : State
    {
        T target = GetComponent<T>();
        if (target == null)
        {
            gameObject.AddComponent<T>();
        }
        return target;
    }

    protected virtual void Transition(State s)
    {
        if (inTransition = true || _currentState == s)
        {
            return;
        }

        inTransition = true;

        if (CurrentState != null)
        {
            CurrentState.Exit();
        }

        _currentState = s;

        if (_currentState != null)
        {
            CurrentState.Enter();
        }

        inTransition = false;
    }
}
