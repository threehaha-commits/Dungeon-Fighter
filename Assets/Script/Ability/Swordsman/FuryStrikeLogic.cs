using UnityEngine;

public class FuryStrikeLogic
{
    private readonly ParticleSystem Effect;
    private readonly float DamagePercent;
    
    public FuryStrikeLogic(ParticleSystem effect, float damagePercent)
    {
        Effect = effect;
        DamagePercent = damagePercent;
    }

    public void Use(Transform target)
    {
        Effect.transform.position = target.position;
        Effect.Play();

        Health enemyHealth = target.GetComponent<Health>();
        IDeathInspector enemyDeathInspector = target.GetComponent<IDeathInspector>();

        float calculatedPercentValue = DamagePercent * enemyHealth.GetCurrentHealth() / 100;
        enemyHealth.ApplyDamage(calculatedPercentValue);
        enemyDeathInspector.ApplyDamage(enemyHealth);
        target.GetComponent<IDeathInspector>().ApplyDamage(enemyHealth);
    }
}