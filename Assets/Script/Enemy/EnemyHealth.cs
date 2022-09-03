using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] private SpriteRenderer HpBar;
    private float FullHpBarLength;

    private void Awake()
    {
        FullHpBarLength = HpBar.size.x;
    }

    protected override void Start()
    {
        base.Start();
        ChangeBar();
    }

    private void OnEnable()
    {
        ChangeBar();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ApplyDamage(10);
    }

    public override void ApplyDamage(float damage)
    {
        base.ApplyDamage(damage);
        ChangeBar();
    }

    protected override void ChangeBar()
    {
        float x = FullHpBarLength * CurrentHealth / MaxHealth;
        HpBar.size = new Vector2(x, 1);
    }

    protected override void Death()
    {
        Debug.Log("Enemy has dead");
        gameObject.SetActive(false);
    }
}
