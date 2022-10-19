using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] protected ItemInfo Info;
    [SerializeField] private int ChanceDrop;
    protected ItemPanel ItemsPanel { get; set; }
    
    public void SetID(int id)
    {
        Info.ID = id;
    }

    public string Name()
    {
        return Info.Name;
    }
    
    public string Description()
    {
        return Info.Description;
    }
    
    public int DropChance()
    {
        return ChanceDrop;
    }
    
    public Sprite Icon() 
    {
        return Info.Icon;
    }
}