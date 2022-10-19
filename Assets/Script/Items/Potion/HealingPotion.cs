using UnityEngine;

public class HealingPotion : ConsumableItem
{
    [SerializeField] private float AmountHealthRestore;
    private Health Health { get; set; }

    private void Start()
    {
        Health = transform.parent.GetComponent<Health>();
    }

    public override void Use()
    {
        base.Use();
        Debug.Log(Health);
        Health.ChangeHpValue(AmountHealthRestore);
    }
}
