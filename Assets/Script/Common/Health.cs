using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IApplyDamage
{
    void ApplyDamage(float damage);
}

public interface IDeathInspector
{
    void ApplyDamage(Health health);
}

public abstract class Health : MonoBehaviour, IApplyDamage
{
    [SerializeField] private float _CurrentHealth;
    protected float CurrentHealth
    {
        get
        {
            return _CurrentHealth;
        }
        set
        {
            _CurrentHealth = value;
            if (_CurrentHealth > MaxHealth)
                _CurrentHealth = MaxHealth;
            if (_CurrentHealth < 0)
                _CurrentHealth = 0;
        }
    }
    [SerializeField] protected float MaxHealth;
    public bool IsDeath 
    {
        get 
        {
            if (CurrentHealth <= 0)
                return true;
            else
                return false;
        }
    }

    protected virtual void Start()
    {
        _CurrentHealth = MaxHealth;
    }


    public void apply()
    {
        Debug.Log("Health");
    }

    public void Restore(float Value)
    {
        CurrentHealth += Value;
        ChangeBar();
    }

    public float GetCurrentHealth()
    {
        return CurrentHealth;
    }

    public float GetMaxHealth()
    {
        return MaxHealth;
    }

    public virtual void ApplyDamage(float damage)
    {
        CurrentHealth = CurrentHealth - damage;
        if (IsDeath)
            Death();
    }

    protected abstract void Death();
    protected abstract void ChangeBar();
}
