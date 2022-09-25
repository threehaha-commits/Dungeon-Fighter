using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class PlayerHealth : Health
{
    private UIDocument Document;
    private ProgressBar HpBar;
    
    [Inject]
    private void Construct(Document document)
    {
        Document = document.Bar;
        HealthChanger = new BarVisualChanger(Document, "HealthBar");
    }

    protected override void Start()
    {
        base.Start();
        HealthChanger.ChangeBar(CurrentHealth, MaxHealth);
    }

    public override void ApplyDamage(float damage)
    {
        base.ApplyDamage(damage);
        HealthChanger.ChangeBar(CurrentHealth, MaxHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ChangeHpValue(-25);
    }

    public override void ChangeHpValue(float Value)
    {
        base.ChangeHpValue(Value);
        HealthChanger.ChangeBar(CurrentHealth, MaxHealth);
    }

    protected override void Death()
    {
        Debug.Log("Player is death");
    }
}