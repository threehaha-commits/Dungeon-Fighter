using UnityEngine;

public class FieryRageLogic : IProlongingEffect
{
    private readonly ParticleSystem HitEffect;
    private readonly ParticleSystem HeadEffect;
    private readonly float DamageAtTheMoment;
    private readonly float DamageFerSecond;
    private float DurationBurning;
    private readonly float BaseDurationBurning;
    private float _prolongDuration;
    float IProlongingEffect.ProlongDuration { get; set; }
    private IProlongingEffect ProlongingEffect;

    public FieryRageLogic(ParticleSystem hitEffect, ParticleSystem headEffect, 
        float damageAtTheMoment, float damageFerSecond, float durationBurning)
    {
        HitEffect = hitEffect;
        HeadEffect = headEffect;
        DamageAtTheMoment = damageAtTheMoment;
        DamageFerSecond = damageFerSecond;
        DurationBurning = durationBurning;
        BaseDurationBurning = durationBurning;
        ProlongingEffect = this;
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

    void IProlongingEffect.ProlongingEffect()
    {
        DurationBurning = BaseDurationBurning + ProlongingEffect.ProlongDuration;
    }
}