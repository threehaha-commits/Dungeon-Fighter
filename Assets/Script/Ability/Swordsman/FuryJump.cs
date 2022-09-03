using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FuryJump : MonoBehaviour, IAbilityTarget
{
    [SerializeField] private AbilityInfo Info;
    public AbilityInfo InfoAbility => Info;
    private TrailRenderer TrailEffect;
    private ParticleSystem BoundEffect;
    [SerializeField] private float DamagePerBound;
    [SerializeField] private int BoundCount;
    [SerializeField] private float TimeBetweenJump;
    private BoxCollider2D Collider;
    private int BoundVariator = 1;
    public Transform Target { get; set; }
    private CharapterState State;
    private float StunTime => TimeBetweenJump * BoundCount;

    private void Awake()
    {
        TrailEffect = (TrailRenderer)Info.Effects.GetEffect(Info.Effects.Trail[0], transform);
        BoundEffect = (ParticleSystem)Info.Effects.GetEffect(Info.Effects.Effect[0], transform);
    }

    private void Start()
    {
        State = GetComponent<CharapterState>();
        Collider = transform.root.GetComponent<BoxCollider2D>();
    }

    void IAbility.Use()
    {
        if (!Info.AbilityMain.AbilityIsReady(Target, transform))
            return;
        TrailEffect.emitting = true;
        State.SetStun(StunTime);
        StartCoroutine(Jumping());
    }

    private IEnumerator Jumping() 
    {
        int boundCount = BoundCount;
        Health enemyHealth = Target.GetComponent<Health>();
        Collider.isTrigger = true;
        IDeathInspector enemyDeathInspector = Target.GetComponent<IDeathInspector>();
        while (boundCount > 0 && enemyHealth.IsDeath == false)
        {
            boundCount--;
            transform.position = CalculateBoundPosition();
            BoundEffect.transform.position = Target.position;
            BoundEffect.Play();
            enemyHealth.ApplyDamage(DamagePerBound);
            yield return new WaitForSeconds(TimeBetweenJump);
        }
        enemyDeathInspector.ApplyDamage(enemyHealth);
        Target.GetComponent<IDeathInspector>().ApplyDamage(enemyHealth);
        TrailEffect.emitting = false;
        Collider.isTrigger = false;
    }

    private Vector2 CalculateBoundPosition() 
    {
        BoundVariator = BoundVariator % 2 == 0 ? -1 : 1;
        float offset = 0.18f;
        float xMin = -offset;
        float xMax = offset;
        float yMin = -offset;
        float yMax = offset;
        float x = Random.Range(Target.position.x + xMin, Target.position.x + xMax);
        float y = Random.Range(Target.position.y + yMin, Target.position.y + yMax);
        return new Vector2(x * BoundVariator, y * BoundVariator);
    }
}
