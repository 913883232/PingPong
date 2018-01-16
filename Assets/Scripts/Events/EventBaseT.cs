﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class EventBase<T> : EventBase {
    private List<Action<T>> _actions;
    public void Publish(T payload)
    {
        if (_actions == null) return;
        foreach (var action in _actions)
        {
            action(payload);
        }
    }
	public void Subscribe(Action<T> action)
    {
        if (_actions ==null)
        {
            _actions = new List<Action<T>>();
        }
        if (!_actions.Contains(action))
        {
            _actions.Add(action);
        }
    }
    public void Unsubscribe(Action<T> action)
    {
        if (_actions ==null)
        {
            return;
        }
        if (_actions.Contains(action))
        {
            _actions.Contains(action);
        }
    }
    public new void Clear()
    {
        if (_actions ==null)
        {
            return;
        }
        _actions.Clear();
        base.Clear();
    }
}
