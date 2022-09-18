using UnityEngine;

public class AttackEnemyLogic
{
    private readonly float Damage;

    public AttackEnemyLogic(float damage)
    {
        Damage = damage;
    }
    
    public void Attack(Transform target)
    {
        IApplyDamage targetApplyDamage = target.GetComponent<IApplyDamage>();
        IDeathInspector deathInspector = target.GetComponent<IDeathInspector>();
        Health healthTarget = target.GetComponent<Health>();
        targetApplyDamage.ApplyDamage(Damage);
        deathInspector?.ApplyDamage(healthTarget);
    }
}