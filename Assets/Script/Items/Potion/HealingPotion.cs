using UnityEngine;

public class HealingPotion : ConsumableItem
{
    [SerializeField] private float AmountHealthRestore;
    private Health Health { get; set; }

    private void Start()
    {
        Health = transform.root.GetComponent<Health>();
    }

    public override void Use()
    {
        base.Use();
        Health.Restore(AmountHealthRestore);
    }
}
