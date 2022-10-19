namespace ItemChange
{
    public interface IConsumable
    {
        void Change(ConsumableItem item, ref int amount, int id);
    }
}

public interface IItemUsable
{
    void Use();
}
