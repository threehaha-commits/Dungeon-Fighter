using UnityEngine;

public class InvisiblePotion : ConsumableItem
{
    [SerializeField] private float DurationInvisible;
    
    public override void Use()
    {
        base.Use();
        Debug.Log("Invisible potion has been used");
    }
}