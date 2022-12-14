using UnityEngine;

public class FieryRage : AbilityInputs, IAbilityTarget
{
    public AbilityInfo InfoAbility { get; set; }
    private ParticleSystem HitEffect;
    private ParticleSystem HeadEffect;
    [SerializeField] private float DamageAtTheMoment;
    [SerializeField] private float DamageFerSecond;
    [SerializeField] private float DurationBurning;
    public Transform Target { get; set; }
    public FieryRageLogic Ability { get; private set; }
    
    private void Awake()
    {
        InputAbility = this;
        InfoAbility = Resources.Load<AbilityInfo>("Ability Object/Fiery Rage");
        HitEffect = (ParticleSystem)InfoAbility.Effects.GetEffect(InfoAbility.Effects.Effect[0], transform);
        HeadEffect = (ParticleSystem)InfoAbility.Effects.GetEffect(InfoAbility.Effects.Effect[1], transform);
        Ability = new FieryRageLogic(HitEffect, HeadEffect, DamageAtTheMoment, DamageFerSecond,
            DurationBurning);
    }


    void IAbility.Use()
    {
        if (InfoAbility.AbilityMain.AbilityIsReady(Target, transform))
        {
            Ability.Use(Target);
        }
    }
}