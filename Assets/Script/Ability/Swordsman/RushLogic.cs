using System.Collections;
using UnityEngine;

public class RushLogic
{
    private readonly AbilityInfo InfoAbility;
    private readonly TrailRenderer Effect;
    private readonly float RushSpeed;
    private readonly float StunDuration;
    private readonly MonoBehaviour Mono;
    private readonly Transform transform;
    
    public RushLogic(MonoBehaviour mono, AbilityInfo infoAbility, Transform transform, TrailRenderer effect, float rushSpeed, float stunDuration)
    {
        Mono = mono;
        InfoAbility = infoAbility;
        this.transform = transform;
        Effect = effect;
        RushSpeed = rushSpeed;
        StunDuration = stunDuration;
    }
    
    public void Use(Transform target)
    {
        Effect.emitting = true;
        target.GetComponent<CharapterState>().SetStun(StunDuration);
        Mono.StartCoroutine(Rushing(target));
    }

    private IEnumerator Rushing(Transform target, float distance = 1f)
    {
        while (distance > InfoAbility.MinRange && target != null)
        {
            distance = (target.position - transform.position).sqrMagnitude;
            transform.position = Vector3.MoveTowards(transform.position, target.position, RushSpeed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
        Effect.emitting = false;
    }
}