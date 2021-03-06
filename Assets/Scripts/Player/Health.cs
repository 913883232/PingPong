﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour,ICanTakeDamage
{
    [SerializeField]
    private int InitialHealth;

    [SerializeField]
    private UnityEvent destroyEvent;

    public int CurrentHealth { get; protected set; }
    public void TakeDamage(int damage,GameObject instigator)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            DestroySelf();
            return;
        }
    }
	void Start () {
        CurrentHealth = InitialHealth;
	}
    public void DestroySelf()
    {
        if (destroyEvent != null)
        {
            destroyEvent.Invoke();
        }
        else Destroy(this.gameObject);
    }
}
