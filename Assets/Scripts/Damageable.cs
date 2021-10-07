using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour { 
    public float health;

    public event System.Action<Vector3> OnHit;
    public event System.Action OnDestruction;

    public Damageable(float health)
    {
        this.health = health;
    }

    /*
    public bool TakeDamage(float amount)
    {
        OnHit?.Invoke();
        health -= amount;

        if(health <= 0)
        {
            OnDestruction?.Invoke();
            Destroy(gameObject);
            return true;
        }

        return false;
    }
    */

    public bool TakeDamage(float amount, Vector3 direction)
    {
        OnHit?.Invoke(direction);
        health -= amount;

        if (health <= 0)
        {
            OnDestruction?.Invoke();
            Destroy(gameObject);
            return true;
        }

        return false;
    }

}
