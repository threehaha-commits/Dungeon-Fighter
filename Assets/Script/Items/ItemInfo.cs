using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemInfo : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    [HideInInspector] public int ID;
    [Multiline] public string Description;
}