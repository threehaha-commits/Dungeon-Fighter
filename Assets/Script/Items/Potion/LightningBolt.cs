using UnityEngine;

public class LightningBolt : NonConsumableItem
{
    public override void Use()
    {
        Debug.Log("Non consumable item was using");
    }
}