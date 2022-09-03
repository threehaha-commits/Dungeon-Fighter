using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class PlayerHealth : Health
{
    private UIDocument Document;
    private ProgressBar HpBar;

    [Inject]
    private void Construct(Document Document)
    {
        this.Document = Document.Bar;
    }
    protected override void Start()
    {
        base.Start();
        var root = Document.rootVisualElement;
        HpBar = root.Q<ProgressBar>("HealthBar");
        ChangeBar();
    }

    public override void ApplyDamage(float damage)
    {
        base.ApplyDamage(damage);
        ChangeBar();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Restore(-25);
    }

    protected override void ChangeBar()
    {
        HpBar.title = $"{Mathf.RoundToInt(CurrentHealth)}/{MaxHealth}";
        HpBar.value = HpBar.highValue * CurrentHealth / MaxHealth;
    }

    protected override void Death()
    {
        Debug.Log("Player is death");
    }
}
