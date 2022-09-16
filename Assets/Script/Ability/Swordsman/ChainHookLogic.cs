using System.Collections;
using UnityEngine;

public class ChainHookLogic
{
    private readonly AbilityInfo Info;
    private readonly float HookSpeed;
    private readonly float StunDuration;
    private readonly ParticleSystem Effect;
    private readonly ChainBehaviour Chain;
    private readonly Transform transform;
    private readonly MonoBehaviour Mono;
    
    public ChainHookLogic(MonoBehaviour mono, AbilityInfo info, float stunDuration, Transform transform, float hookSpeed, ParticleSystem effect, ChainBehaviour chain)
    {
        Mono = mono;
        Info = info;
        StunDuration = stunDuration;
        this.transform = transform;
        HookSpeed = hookSpeed;
        Effect = effect;
        Chain = chain;
    }
    
    public void Use(Transform target)
    {
        Chain.Active(target, transform, Info.MinRange, Effect);
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
}