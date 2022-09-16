using UnityEngine;

public class FieryRageLogic
{
    private readonly AbilityInfo Info;
    private readonly ParticleSystem HitEffect;
    private readonly ParticleSystem HeadEffect;
    private readonly float DamageAtTheMoment;
    private readonly float DamageFerSecond;
    private readonly float DurationBurning;
    
    public FieryRageLogic(AbilityInfo info,  ParticleSystem hitEffect, ParticleSystem headEffect, 
        float damageAtTheMoment, float damageFerSecond, float durationBurning)
    {
        Info = info;
        HitEffect = hitEffect;
        HeadEffect = headEffect;
        DamageAtTheMoment = damageAtTheMoment;
        DamageFerSecond = damageFerSecond;
        DurationBurning = durationBurning;
    }
    
    public void Use(Transform target)
    {
        HitEffect.transform.position = target.position;
        HitEffect.Play();

        IDeathInspector enemyDeathInspector = target.GetComponent<IDeathInspector>();
        Health enemyHealth = target.GetComponent<Health>();

        target.GetComponent<CharapterState>().StartBurning(DurationBurning, DamageFerSecond, HeadEffect);
        enemyHealth.ApplyDamage(DamageAtTheMoment);
        
        enemyDeathInspector.ApplyDamage(enemyHealth);
        target.GetComponent<IDeathInspector>().ApplyDamage(enemyHealth);
    }
}