using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestructibleObject : MonoBehaviour, IAgent, IHittable
{
    // Implementing IAgent properties
    public int Health { get; private set; } = 100;
    public UnityEvent OnDie { get; set; } = new UnityEvent();
    public UnityEvent OnGetHit { get; set; } = new UnityEvent();

    // Implementing IHittable method
    public void GetHit(int damage, GameObject damageDealer)
    {
        // Trigger the hit event
        OnGetHit.Invoke();

        // Apply the damage to health
        TakeDamage(damage);
    }

    // Damage handling
    private void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            OnDie.Invoke(); // Trigger destruction event
            DestroyObject();
        }
    }

    private void DestroyObject()
    {
        // Add any visual/audio effects here if needed (like particle systems or sounds)
        Debug.Log(gameObject.name + " destroyed!");
        Destroy(gameObject);
    }

    private void Start()
    {
        // Optional: Set up some basic listeners for hit and die events
        OnDie.AddListener(() => Debug.Log(gameObject.name + " has been destroyed!"));
        OnGetHit.AddListener(() => Debug.Log(gameObject.name + " got hit by something!"));
    }
}
