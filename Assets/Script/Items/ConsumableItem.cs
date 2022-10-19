using UnityEngine;

public class ConsumableItem : Item, IItemUsable
{
    [SerializeField] private int Amount;
    private ConsumableHandler ConsumableHandler;
    private ConsumableVisual ConsumableVisual;
    private delegate void ItemUse(ConsumableItem item, ref int amount, int id);
    private ItemUse ItemUser;

    public void Construct(ConsumableHandler handler, ConsumableVisual visual, ItemPanel itemPanel)
    {
        ConsumableHandler = handler;
        ConsumableVisual = visual;
        ItemsPanel = itemPanel;
        ItemsPanel.AddItem(this);
    }
    
    public void Increase(int amount)
    {
        Amount += amount;
    }

    public int GetAmount()
    {
        return Amount;
    }
    
    public void Refresh()
    {
        ConsumableVisual.Change(this, ref Amount, Info.ID);
    }
    
    public void Initialize()
    {
        ItemUser += ConsumableHandler.Change;
        ItemUser += ConsumableVisual.Change;
        ConsumableVisual.Change(this, ref Amount,  Info.ID);
    }

    public virtual void Use()
    {
        ItemUser?.Invoke(this, ref Amount,  Info.ID);
    }

    private void OnDestroy()
    {
        ItemUser -= ConsumableHandler.Change;
        ItemUser -= ConsumableVisual.Change;
        ItemsPanel.RemoveItem(this,  Info.ID);
    }
}