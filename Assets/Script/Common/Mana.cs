using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    [SerializeField] private float _CurrentMana;
    protected float CurrentMana
    {
        get
        {
            return _CurrentMana;
        }
        set
        {
            _CurrentMana = value;
            if (_CurrentMana > MaxMana)
                _CurrentMana = MaxMana;
            if (_CurrentMana < 0)
                _CurrentMana = 0;
        }
    }
    [SerializeField] protected float MaxMana;

    protected virtual void Start()
    {
        _CurrentMana = MaxMana;
    }

    public virtual void Restore(float Value)
    {
        CurrentMana += Value;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Restore(-25);
    }

    public float GetCurrentHealth()
    {
        return CurrentMana;
    }

    public float GetMaxHealth()
    {
        return MaxMana;
    }
}
