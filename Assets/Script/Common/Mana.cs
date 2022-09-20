using UnityEngine;

public class Mana : MonoBehaviour
{
    [SerializeField] private float _CurrentMana;
    protected float CurrentMana
    {
        get => _CurrentMana;
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
    protected BarVisualChanger ManaChanger;
    
    protected virtual void Start()
    {
        _CurrentMana = MaxMana;
    }

    public virtual void ChangeManaValue(float Value)
    {
        CurrentMana += Value;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ChangeManaValue(-25);
    }
}
