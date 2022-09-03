using UnityEngine;

public class ExperiencePotion : ConsumableItem
{
    [SerializeField] private float AmountExperienceGiven;
    
    public override void Use()
    {
        base.Use();
        Debug.Log("Experience potion has been used");
    }
}