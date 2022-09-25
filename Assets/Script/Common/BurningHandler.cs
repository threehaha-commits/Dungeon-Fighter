using System.Collections;
using UnityEngine;

public class PeriodicDamageHandler : IPeriodicDamageable
{
    public MonoBehaviour Mono { get; set; }
    private readonly IPeriodicDamageable PeriodicDamageable;
    private readonly Health Health;
    private readonly Transform Target;
    
    public PeriodicDamageHandler(MonoBehaviour mono, Transform target)
    {
        PeriodicDamageable = this;
        Mono = mono;
        Target = target;
        Health = Target.GetComponent<Health>();
    }

    void IPeriodicDamageable.StartPeriodicDamage(float duration, float damagePerSecond, ParticleSystem effect)
    {
        Mono.StartCoroutine(PeriodicDamageable.PeriodicDamage(duration, damagePerSecond, effect));
    }

    IEnumerator IPeriodicDamageable.PeriodicDamage(float duration, float damagePerSecond, ParticleSystem effect)
    {
        var position = effect.transform.localPosition;
        effect.transform.parent = Target;
        effect.transform.localPosition = position;
        IDeathInspector deathInspector = Target.GetComponent<IDeathInspector>();
        while (duration > 0)
        {
            effect.Play();
            Health?.ApplyDamage(damagePerSecond);
            deathInspector?.ApplyDamage(Health);
            duration--;
            yield return new WaitForSeconds(1f);
        }
        effect.transform.parent = Target.parent;
        effect.Stop();
    }
}