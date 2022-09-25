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
        get => _CurrentHealth;
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
    public bool IsDeath => CurrentHealth <= 0;

    protected BarVisualChanger HealthChanger;
    
    protected virtual void Start()
    {
        _CurrentHealth = MaxHealth;
    }
    
    public virtual void ChangeHpValue(float Value)
    {
        CurrentHealth += Value;
    }
    
    public void ChangeMaxHealthValue(float value)
    {
        MaxHealth += value;
        CurrentHealth = MaxHealth;
        HealthChanger.ChangeBar(CurrentHealth, MaxHealth);
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
        CurrentHealth -= damage;
        if (IsDeath)
            Death();
    }

    protected abstract void Death();
}
