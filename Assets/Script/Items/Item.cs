using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] protected ItemInfo Info;
    [SerializeField] private int DropChance;
    protected ItemPanel ItemsPanel { get; set; }

    public void SetID(int id)
    {
        Info.ID = id;
    }

    public string GetNameInfo()
    {
        return Info.Name;
    }
    
    public string GetDescriptionInfo()
    {
        return Info.Description;
    }
    
    public int GetDropChance()
    {
        return DropChance;
    }
    
    public Sprite GetIcon() 
    {
        return Info.Icon;
    }
}
