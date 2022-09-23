using UnityEngine;

public class FieryRage : MonoBehaviour, IAbilityTarget
{
    public AbilityInfo InfoAbility { get; set; }
    private ParticleSystem HitEffect;
    private ParticleSystem HeadEffect;
    [SerializeField] private float DamageAtTheMoment;
    [SerializeField] private float DamageFerSecond;
    [SerializeField] private float DurationBurning;
    public Transform Target { get; set; }
    private FieryRageLogic Ability;
    
    private void Awake()
    {
        InfoAbility = Resources.Load<AbilityInfo>("Ability Object/Fiery Rage");
        HitEffect = (ParticleSystem)InfoAbility.Effects.GetEffect(InfoAbility.Effects.Effect[0], transform);
        HeadEffect = (ParticleSystem)InfoAbility.Effects.GetEffect(InfoAbility.Effects.Effect[1], transform);
        Ability = new FieryRageLogic(HitEffect, HeadEffect, DamageAtTheMoment, DamageFerSecond,
            DurationBurning);
    }


    void IAbility.Use()
    {
        if (InfoAbility.AbilityMain.AbilityIsReady(Target, transform))
            Ability.Use(Target);
    }
}