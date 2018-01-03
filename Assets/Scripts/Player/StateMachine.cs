using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateMachine<TLabel>
{
    #region  Types

    private class State
    {
        #region Public Fields

        public readonly TLabel label;
        public readonly Action onStart;
        public readonly Action onStop;
        public readonly Action onUpdate;

        #endregion

        #region Constructors

        public State(TLabel label, Action onStart, Action onUpdate, Action onStop)
        {
            this.onStart = onStart;
            this.onUpdate = onUpdate;
            this.onStop = onStop;
            this.label = label;
        }

        #endregion
    }

    #endregion

    #region Private Fields

    private readonly Dictionary<TLabel, State> stateDictionary;
    private State currentState;

    #endregion

    #region  Properties
    public TLabel CurrentState
    {
        get { return currentState.label; }

        set { ChangeState(value); }
    }

    #endregion

    #region Constructors
    
    public StateMachine()
    {
        stateDictionary = new Dictionary<TLabel, State>();
    }

    #endregion

    #region Unity Callbacks
    
    public void Update()
    {
        if (currentState != null && currentState.onUpdate != null)
        {
            currentState.onUpdate();
        }
    }

    #endregion

    #region Public Methods
    
    public void AddState(TLabel label, Action onStart, Action onUpdate, Action onStop)
    {
        stateDictionary[label] = new State(label, onStart, onUpdate, onStop);
    }
    public void AddState(TLabel label, Action onStart, Action onUpdate)
    {
        AddState(label, onStart, onUpdate, null);
    }
    public void AddState(TLabel label, Action onStart)
    {
        AddState(label, onStart, null);
    }
    public void AddState(TLabel label)
    {
        AddState(label, null);
    }
    public void AddState<TSubStateLabel>(TLabel label, StateMachine<TSubStateLabel> subMachine,
        TSubStateLabel subMachineStartState)
    {
        AddState(
            label,
            () => subMachine.ChangeState(subMachineStartState),
            subMachine.Update);
    }
    public override string ToString()
    {
        return CurrentState.ToString();
    }

    #endregion

    #region Private Methods
    private void ChangeState(TLabel newState)
    {
        if (currentState != null && currentState.onStop != null)
        {
            currentState.onStop();
        }

        currentState = stateDictionary[newState];

        if (currentState.onStart != null)
        {
            currentState.onStart();
        }
    }

    #endregion
}

