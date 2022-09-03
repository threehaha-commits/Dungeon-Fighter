using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieryRage : MonoBehaviour, IAbilityTarget
{
    [SerializeField] private AbilityInfo Info;
    public AbilityInfo InfoAbility => Info;
    private ParticleSystem HitEffect;
    private ParticleSystem HeadEffect;
    [SerializeField] private float DamageAtTheMoment;
    [SerializeField] private float DamageFerSecond;
    [SerializeField] private float DurationBurning;
    public Transform Target { get; set; }

    private void Awake()
    {
        HitEffect = (ParticleSystem)Info.Effects.GetEffect(Info.Effects.Effect[0], transform);
        HeadEffect = (ParticleSystem)Info.Effects.GetEffect(Info.Effects.Effect[1], transform);
    }

    void IAbility.Use()
    {
        if (!Info.AbilityMain.AbilityIsReady(Target, transform))
            return;
        HitEffect.transform.position = Target.position;
        HitEffect.Play();

        IDeathInspector enemyDeathInspector = Target.GetComponent<IDeathInspector>();
        Health enemyHealth = Target.GetComponent<Health>();

        Target.GetComponent<CharapterState>().StartBurning(DurationBurning, DamageFerSecond, HeadEffect);
        enemyHealth.ApplyDamage(DamageAtTheMoment);
        
        enemyDeathInspector.ApplyDamage(enemyHealth);
        Target.GetComponent<IDeathInspector>().ApplyDamage(enemyHealth);
    }
}
