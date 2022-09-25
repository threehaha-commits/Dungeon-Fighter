using System.Collections;
using UnityEngine;

public class FuryJumpLogic : IValueChanger<FuryJumpUpgradeCountBound, int>, IValueChanger<FuryJumpUpgradeDamagePerBound, float>
{
    private readonly TrailRenderer TrailEffect;
    private readonly ParticleSystem BoundEffect;
    private float DamagePerBound;
    private int BoundCount;
    private readonly float TimeBetweenJump;
    private readonly BoxCollider2D Collider;
    private int BoundVariator = 1;
    private readonly CharapterState State;
    private float StunTime => TimeBetweenJump * BoundCount;
    private readonly MonoBehaviour Mono;
    private readonly Transform transform;
    
    public FuryJumpLogic(MonoBehaviour mono, Transform transform, TrailRenderer trailEffect, ParticleSystem boundEffect,
        float damagePerBound, int boundCount, float timeBetweenJump, BoxCollider2D collider, CharapterState state)
    {
        Mono = mono;
        this.transform = transform;
        TrailEffect = trailEffect;
        BoundEffect = boundEffect;
        DamagePerBound = damagePerBound;
        BoundCount = boundCount;
        TimeBetweenJump = timeBetweenJump;
        Collider = collider;
        State = state;
    }
    
    public void Use(Transform target)
    {
        TrailEffect.emitting = true;
        State.SetStun(StunTime);
        Mono.StartCoroutine(Jumping(target));
    }

    private IEnumerator Jumping(Transform target) 
    {
        int boundCount = BoundCount;
        Health enemyHealth = target.GetComponent<Health>();
        Collider.isTrigger = true;
        IDeathInspector enemyDeathInspector = target.GetComponent<IDeathInspector>();
        while (boundCount > 0 && enemyHealth.IsDeath == false)
        {
            boundCount--;
            transform.position = CalculateBoundPosition(target);
            BoundEffect.transform.position = target.position;
            BoundEffect.Play();
            enemyHealth.ApplyDamage(DamagePerBound);
            yield return new WaitForSeconds(TimeBetweenJump);
        }
        enemyDeathInspector.ApplyDamage(enemyHealth);
        target.GetComponent<IDeathInspector>().ApplyDamage(enemyHealth);
        TrailEffect.emitting = false;
        Collider.isTrigger = false;
    }

    private Vector2 CalculateBoundPosition(Transform target) 
    {
        BoundVariator = BoundVariator % 2 == 0 ? -1 : 1;
        float offset = 0.18f;
        float xMin = -offset;
        float xMax = offset;
        float yMin = -offset;
        float yMax = offset;
        float x = Random.Range(target.position.x + xMin, target.position.x + xMax);
        float y = Random.Range(target.position.y + yMin, target.position.y + yMax);
        return new Vector2(x * BoundVariator, y * BoundVariator);
    }

    void IValueChanger<FuryJumpUpgradeCountBound, int>.ChangeValue(int value)
    {
        BoundCount += value;
        Debug.Log($"BoundCount {BoundCount}");
    }

    void IValueChanger<FuryJumpUpgradeDamagePerBound, float>.ChangeValue(float value)
    {
        DamagePerBound += value;
        Debug.Log($"DamagePerBound {DamagePerBound}");
    }
}