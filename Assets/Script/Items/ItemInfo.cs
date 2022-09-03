using UnityEngine;

public enum ItemType
{
    Consumable,
    NonConsumable
};

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemInfo : ScriptableObject
{
    public ItemType Type;
    public string Name;
    public Sprite Icon;
    [HideInInspector] public int ID;
    [Multiline] public string Description;
}