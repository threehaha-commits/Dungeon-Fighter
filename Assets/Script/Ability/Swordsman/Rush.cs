using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class Rush : MonoBehaviour, IAbilityTarget
{
    [SerializeField] private AbilityInfo Info;
    public AbilityInfo InfoAbility => Info;
    private TrailRenderer Effect;
    [SerializeField] private float RushSpeed = 3f;
    private float StunDuration = 2.75f;
    public Transform Target { get; set; }
    
    private void Awake()
    {
        Effect = (TrailRenderer)Info.Effects.GetEffect(Info.Effects.Trail[0], transform);
    }
    
    void IAbility.Use()
    {
        if (!Info.AbilityMain.AbilityIsReady(Target, transform))
            return;
        Effect.emitting = true;
        Target.GetComponent<CharapterState>().SetStun(StunDuration);
        StartCoroutine(Rushing());
    }

    private IEnumerator Rushing(float distance = 1f)
    {
        while (distance > Info.MinRange && Target != null)
        {
            distance = (Target.position - transform.position).sqrMagnitude;
            transform.position = Vector3.MoveTowards(transform.position, Target.position, RushSpeed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
        Effect.emitting = false;
    }
}
