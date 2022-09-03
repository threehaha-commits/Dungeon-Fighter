using System.Collections;
using UnityEngine;

public class BurningHandler : IBurning
{
    public MonoBehaviour Mono { get; set; }
    private IBurning IBurning;
    private Transform Head;
    private Health Health;
    private IStateHandler Stun;
    private Transform Target;
    
    public BurningHandler(MonoBehaviour mono, Transform target)
    {
        IBurning = this;
        Mono = mono;
        Target = target;
        Head = Target.Find("Head");
        Health = Target.GetComponent<Health>();
    }

    void IBurning.Burn(float duration, float damagePerSecond, ParticleSystem effect)
    {
        Mono.StartCoroutine(IBurning.Burning(duration, damagePerSecond, effect));
    }

    public IEnumerator Burning(float duration, float damagePerSecond, ParticleSystem effect)
    {
        effect.transform.parent = Head;
        effect.transform.localPosition = Vector2.zero;
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