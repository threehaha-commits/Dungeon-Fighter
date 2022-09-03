using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuryStrike : MonoBehaviour, IAbilityTarget
{
    [SerializeField] private AbilityInfo Info;
    public AbilityInfo InfoAbility => Info;
    private ParticleSystem Effect;
    [SerializeField] private float DamagePercent;
    public Transform Target { get; set; }

    private void Awake()
    {
        Effect = (ParticleSystem)Info.Effects.GetEffect(Info.Effects.Effect[0], transform);
    }

    void IAbility.Use()
    {
        if (!Info.AbilityMain.AbilityIsReady(Target, transform))
            return;
        Effect.transform.position = Target.position;
        Effect.Play();

        Health enemyHealth = Target.GetComponent<Health>();
        IDeathInspector enemyDeathInspector = Target.GetComponent<IDeathInspector>();

        float calculatedPercentValue = DamagePercent * enemyHealth.GetCurrentHealth() / 100;
        enemyHealth.ApplyDamage(calculatedPercentValue);
        enemyDeathInspector.ApplyDamage(enemyHealth);
        Target.GetComponent<IDeathInspector>().ApplyDamage(enemyHealth);
    }
}
