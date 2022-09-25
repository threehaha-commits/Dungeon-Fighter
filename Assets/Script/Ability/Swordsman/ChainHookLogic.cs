using System.Collections;
using UnityEngine;

public class ChainHookLogic : IActioner<ChainHookDealsDamage, float>, IActioner<ChainHookStunDurationUpgrade, float>
{
    private readonly AbilityInfo Info;
    private readonly float HookSpeed;
    private float StunDuration = 1.25f;
    private readonly ParticleSystem Effect;
    private readonly ChainBehaviour Chain;
    private readonly Transform transform;
    private readonly MonoBehaviour Mono;
    private readonly IActioner<ChainHookStunDurationUpgrade, float> DurationUpgrade;
    private readonly IActioner<ChainHookDealsDamage, float> GetterDamageable;
    float IActioner<ChainHookStunDurationUpgrade, float>.Value { get; set; }
    float IActioner<ChainHookDealsDamage, float>.Value { get; set; } = 5;

    private Transform Target;
    
    public ChainHookLogic(MonoBehaviour mono, AbilityInfo info, Transform transform, float hookSpeed, ParticleSystem effect, ChainBehaviour chain)
    {
        Mono = mono;
        Info = info;
        this.transform = transform;
        HookSpeed = hookSpeed;
        Effect = effect;
        Chain = chain;
        DurationUpgrade = this;
        GetterDamageable = this;
    }
    
    public void Use(Transform target)
    {
        Target = target;
        Chain.Activate(target, transform, Info.MinRange, Effect);
        target.GetComponent<CharapterState>().SetStun(StunDuration);
        Mono.StartCoroutine(Hooking(target));
    }
    
    private IEnumerator Hooking(Transform target, float distance = 1f)
    {
        while (distance > Info.MinRange && target != null)
        {
            distance = (transform.position - target.position).sqrMagnitude;
            target.position = Vector3.MoveTowards(target.position, transform.position,
                HookSpeed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
    
    void IActioner<ChainHookStunDurationUpgrade, float>.Action()
    {
        StunDuration += DurationUpgrade.Value;
    }

    void IActioner<ChainHookDealsDamage, float>.Action()
    {
        IDeathInspector enemyDeathInspector = Target.GetComponent<IDeathInspector>();
        Health enemyHealth = Target.GetComponent<Health>();

        enemyHealth.ApplyDamage(GetterDamageable.Value);
        
        enemyDeathInspector.ApplyDamage(enemyHealth);
        Target.GetComponent<IDeathInspector>().ApplyDamage(enemyHealth);
        
        Debug.Log("Player deal damage to enemy");
    }
}