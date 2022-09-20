using UnityEngine;

public class ExperiencePotion : ConsumableItem
{
    [SerializeField] private int AmountExperience;
    private IExpirience ExpirienceFromCharapter;

    private void Start()
    {
        ExpirienceFromCharapter = transform.root.GetComponent<IExpirience>();
    }

    public override void Use()
    {
        base.Use();
        ExpirienceFromCharapter.SetExp(AmountExperience);
        Debug.Log("Experience potion has been used");
    }
}