using System.Collections;
using UnityEngine;

public class EnemySimpleAttackConroller : MonoBehaviour
{
    [SerializeField] private ParticleSystem EffectToTarget;
    [SerializeField] private ParticleSystem EffectFromEnemy;
    [SerializeField] private float Damage;
    private Transform Target;
    private IEnumerator AttackCoroutine;
    private AttackEnemyLogic AttackLogic;
    private EffectController EffectPlayController;
    
    private void Start()
    {
        EffectPlayController = new EffectController(EffectToTarget, EffectFromEnemy);
        AttackLogic = new AttackEnemyLogic(Damage);
        AttackCoroutine = Attack();
    }

    public void AttackTarget(Transform target)
    {
        Target = target;
        StartCoroutine(AttackCoroutine);
    }

    public void StopAttack()
    {
        StopCoroutine(AttackCoroutine);
    }
    
    private IEnumerator Attack()
    {
        while(true)
        {
            EffectPlayController.Play(Target);
            AttackLogic.Attack(Target);
            yield return new WaitForSeconds(1f);
        }
    }

    private class EffectController
    {
        private readonly ParticleSystem EffectToTarget;
        private readonly ParticleSystem EffectFromEnemy;
        public EffectController(ParticleSystem effect, ParticleSystem effectFromEnemy)
        {
            EffectToTarget = effect;
            EffectFromEnemy = effectFromEnemy;
        }

        public void Play(Transform target)
        {
            EffectToTarget.transform.position = target.position;
            EffectToTarget.Play();
            EffectFromEnemy.Play();
        }
    }
}