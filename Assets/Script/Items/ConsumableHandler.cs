using UnityEngine;
using Zenject;

public interface IConsumable
{
    void Change(ConsumableItem item, ref int amount, int id);
}

public class ConsumableHandler : IConsumable
{
    private ItemPanel Keeper;
    private Reloader Reload;
    private float ReloadTime = 3.0f;
    
    public ConsumableHandler(ItemPanel keeper)
    {
        Keeper = keeper;
        Reload = new Reloader(ReloadTime);
    }
    
    public void Change(ConsumableItem item, ref int amount, int id)
    {
        if (Reload.Reloaded == false)
            return;
        
        amount--;
        
        if (amount == 0)
        {
            Keeper.RemoveItem(item, id);
            return;
        }
        
        Reload.StartReload();
    }
}