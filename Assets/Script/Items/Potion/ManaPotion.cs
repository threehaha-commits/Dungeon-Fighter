using UnityEngine;

public class ManaPotion : ConsumableItem
{
    [SerializeField] private float AmountManaRestore;
    private Mana Mana { get; set; }

    private void Start()
    {
        Mana = transform.root.GetComponent<Mana>();
    }

    public override void Use()
    {
        base.Use();
        Mana.ChangeManaValue(AmountManaRestore);
    }
}