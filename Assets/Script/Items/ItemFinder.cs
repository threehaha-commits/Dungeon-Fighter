using System.Collections.Generic;

public class SlotFinder
{
    public int FindEmptySlot(int slotCount, Dictionary<int, Item> panelWithItems)
    {
        for (int i = 0; i < slotCount; i++)
        {
            if (panelWithItems.ContainsKey(i) == false)
                return i;
        }

        return -1;
    }

    public int FindIdenticalConsumableItems(Dictionary<int, Item> panelWithItems, Item item)
    {
        for (int i = 0; i < panelWithItems.Count; i++)
        {
            string itemName = panelWithItems[i].name;
            if (itemName == item.name)
                return i;
        }

        return -1;
    }
}