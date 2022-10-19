using UnityEngine;

public class ChainHook : AbilityInputs, IAbilityTarget, ISetterNewEffect<IActioner<ChainHookDealsDamage, float>>
{
    public AbilityInfo InfoAbility { get; set; }
    private ParticleSystem Effect;
    private ChainBehaviour Chain;
    [SerializeField] private float HookSpeed;
    public Transform Target { get; set; }
    public ChainHookLogic Ability { get; private set; }
    private IActioner<ChainHookDealsDamage, float> GetterDamageable;
    
    private void Awake()
    {
        InputAbility = this;
        InfoAbility = Resources.Load<AbilityInfo>("Ability Object/Chain Hook");
        Effect = (ParticleSystem)InfoAbility.Effects.GetEffect(InfoAbility.Effects.Effect[0], transform);
        Chain = InfoAbility.Effects.GetEffect<ChainBehaviour>(InfoAbility.Effects.Line[0], transform);
        Ability = new ChainHookLogic(this, InfoAbility, transform, HookSpeed, Effect, Chain);
    }

    void IAbility.Use()
    {
        if (InfoAbility.AbilityMain.AbilityIsReady(Target, transform))
        {
            Ability.Use(Target);
            GetterDamageable?.Action();
        }
    }

    void ISetterNewEffect<IActioner<ChainHookDealsDamage, float>>.SetNewEffect(IActioner<ChainHookDealsDamage, float> upgradeAbility)
    {
        GetterDamageable = upgradeAbility;
    }
}