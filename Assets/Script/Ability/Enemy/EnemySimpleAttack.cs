using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimpleAttack : MonoBehaviour
{
    [SerializeField] private ParticleSystem Effect;
    [SerializeField] private float Damage;

    private void Start()
    {
        //ThisSkill = this;
    }

    void Use()
    {
        //Effect.transform.position = Target.position;
        Effect.Play();
        //IApplyDamage targetApplyDamage = Target.GetComponent<IApplyDamage>();
        //targetApplyDamage.ApplyDamage(Damage);
    }
}
