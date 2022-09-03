using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill : MonoBehaviour
{
    protected Transform Target;
    protected EnemySkill ThisSkill;

    public void SetTarget(Transform Target)
    {
        this.Target = Target;
    }

    private void FixedUpdate()
    {
        //ThisSkill?.Use();
    }
}
