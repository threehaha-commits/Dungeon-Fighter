public class NonConsumableItem : Item, IItemUsable
{
    public void Construct(ItemPanel itemPanel)
    {
        ItemsPanel = itemPanel;
        ItemsPanel.AddItem(this);
    }
    
    public virtual void Use()
    {
        
    }
}