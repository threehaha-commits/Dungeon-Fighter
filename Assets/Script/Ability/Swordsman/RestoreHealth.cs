using System;
using UnityEngine;

public class RestoreHealth : AbilityInputs, IAbility, ISetterNewEffect<IActioner<RestoreHealthIncreaseMaxHealth, float>>
{
    public AbilityInfo InfoAbility { get; set; }
    private ParticleSystem Effect;
    [SerializeField] private float LifeRestore;
    private Health _Health;
    public RestoreHealthLogic Ability { get; private set; }
    private IActioner<RestoreHealthIncreaseMaxHealth, float> ActionerMaxValue;
    
    private void Awake()
    {
        InfoAbility = Resources.Load<AbilityInfo>("Ability Object/Restore Health");
        Effect = (ParticleSystem)InfoAbility.Effects.GetEffect(InfoAbility.Effects.Effect[0], transform);
    }

    private void Start()
    {
        InputAbility = this;
        _Health = GetComponent<Health>();
        Ability = new RestoreHealthLogic(Effect, _Health, LifeRestore);
    }

    void IAbility.Use()
    {
        if (InfoAbility.AbilityMain.AbilityIsReady())
        {
            Ability.Use();
            ActionerMaxValue?.Action();
        }
    }

    void ISetterNewEffect<IActioner<RestoreHealthIncreaseMaxHealth, float>>.SetNewEffect(IActioner<RestoreHealthIncreaseMaxHealth, float> upgradeAbility)
    {
        ActionerMaxValue = upgradeAbility;
    }
}

public class AbilityInputs : MonoBehaviour
{
    public KeyCode Key;
    protected IAbility InputAbility;
    
    protected virtual void Update()
    {
        if (Input.GetKeyDown(Key))
        {
            InputAbility.Use();
        }
    }
}