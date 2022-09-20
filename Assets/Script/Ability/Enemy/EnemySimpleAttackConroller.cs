using System.Collections;
using UnityEngine;

public class EnemySimpleAttackConroller : MonoBehaviour
{
    [SerializeField] private ParticleSystem EffectToTarget;
    [SerializeField] private ParticleSystem EffectFromEnemy;
    [SerializeField] private float Damage;
    [SerializeField] private float ReloadTime;
    private Transform Target;
    private IEnumerator AttackCoroutine;
    private AttackEnemyLogic AttackLogic;
    private EffectController EffectPlayController;
    private Reloader Reload;
    
    private void Start()
    {
        Reload = new Reloader(ReloadTime);
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
            if(Reload.Reloaded)
            {
                EffectPlayController.Play(Target);
                AttackLogic.Attack(Target);
                Reload.StartReload();
            }
            yield return null;
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