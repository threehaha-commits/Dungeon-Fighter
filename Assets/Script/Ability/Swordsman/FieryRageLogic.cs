using UnityEngine;

public class FieryRageLogic : IActioner<float>, IValueChanger<FieryRageMomentDamageUpgrade, float>, IValueChanger<FieryRageDamagePerSecondUpgrade, float>
{
    private readonly ParticleSystem HitEffect;
    private readonly ParticleSystem HeadEffect;
    private float DamageAtTheMoment;
    private float DamageFerSecond;
    private float DurationBurning;
    private float _prolongDuration;
    float IActioner<float>.Value { get; set; }
    private IActioner<float> DurationUpgrade;

    public FieryRageLogic(ParticleSystem hitEffect, ParticleSystem headEffect, 
        float damageAtTheMoment, float damageFerSecond, float durationBurning)
    {
        HitEffect = hitEffect;
        HeadEffect = headEffect;
        DamageAtTheMoment = damageAtTheMoment;
        DamageFerSecond = damageFerSecond;
        DurationBurning = durationBurning;
        DurationUpgrade = this;
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

    void IActioner<float>.Action()
    {
        DurationBurning += DurationUpgrade.Value;
        Debug.Log(DurationBurning);
    }

    void IValueChanger<FieryRageMomentDamageUpgrade, float>.ChangeValue(float value)
    {
        DamageAtTheMoment += value;
        Debug.Log($"DamageAtTheMoment {DamageAtTheMoment}");
    }

    void IValueChanger<FieryRageDamagePerSecondUpgrade, float>.ChangeValue(float value)
    {
        DamageFerSecond += value;
        Debug.Log($"DamageFerSecond {DamageFerSecond}");
    }
}