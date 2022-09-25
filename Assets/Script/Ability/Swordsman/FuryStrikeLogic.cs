using UnityEngine;

public class FuryStrikeLogic : IValueChanger<float>, IGetterPeriodicDamageable
{
    private readonly ParticleSystem Effect;
    private float DamagePercent;
    private Transform Target;
    private IGetterPeriodicDamageable GetterPeriodicDamageable;
    float IGetterPeriodicDamageable.Duration { get; set; } = 4;
    float IGetterPeriodicDamageable.DamagePerSecond { get; set; } = 3f;
    ParticleSystem IGetterPeriodicDamageable.Effect { get; set; }
    
    public FuryStrikeLogic(ParticleSystem effect, float damagePercent)
    {
        Effect = effect;
        DamagePercent = damagePercent;
        GetterPeriodicDamageable = this;
    }

    public void Use(Transform target)
    {
        Target = target;
        Effect.transform.position = target.position;
        Effect.Play();

        Health enemyHealth = target.GetComponent<Health>();
        IDeathInspector enemyDeathInspector = target.GetComponent<IDeathInspector>();

        float calculatedPercentValue = DamagePercent * enemyHealth.GetCurrentHealth() / 100;
        enemyHealth.ApplyDamage(calculatedPercentValue);
        enemyDeathInspector.ApplyDamage(enemyHealth);
        target.GetComponent<IDeathInspector>().ApplyDamage(enemyHealth);
    }

    void IValueChanger<float>.ChangeValue(float value)
    {
        DamagePercent += value;
    }

    void IGetterPeriodicDamageable.StartPeriodicDamage()
    {
        Target.GetComponent<CharapterState>().StartBurning(GetterPeriodicDamageable.Duration, GetterPeriodicDamageable.DamagePerSecond, GetterPeriodicDamageable.Effect);
    }
}