using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] private SpriteRenderer HpRenderer;
    private float FullHpBarLength;

    private void Awake()
    {
        FullHpBarLength = HpRenderer.size.x;
        HealthChanger = new BarVisualChanger(HpRenderer);
    }

    protected override void Start()
    {
        base.Start();
        HealthChanger.ChangeBar(FullHpBarLength, CurrentHealth, MaxHealth);
    }

    private void OnEnable()
    {
        HealthChanger.ChangeBar(FullHpBarLength, CurrentHealth, MaxHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ApplyDamage(10);
    }

    public override void ChangeHpValue(float Value)
    {
        base.ChangeHpValue(Value);
        HealthChanger.ChangeBar(FullHpBarLength, CurrentHealth, MaxHealth);
    }

    public override void ApplyDamage(float damage)
    {
        base.ApplyDamage(damage);
        HealthChanger.ChangeBar(FullHpBarLength, CurrentHealth, MaxHealth);
    }
    
    protected override void Death()
    {
        Debug.Log("Enemy has dead");
        gameObject.SetActive(false);
    }
}
