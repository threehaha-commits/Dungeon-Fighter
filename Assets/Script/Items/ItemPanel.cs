using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

//In development
public class ItemPanel : MonoBehaviour
{
    private VisualElement Root;
    private Dictionary<int, Item> PanelWithItems = new();
    private Button[] Slot = new Button[9];
    private VisualElement[] ConsumableVisual = new VisualElement[9];
    private int SlotCount;
    private SlotFinder Finder = new();
    
    [Inject]
    private void Construct(Document document)
    {
        MouseOverUI.AddElement(document.ItemPanel, "ItemPanel");
        Root = document.ItemPanel.rootVisualElement;
        SlotCount = Root.Q<VisualElement>("ItemPanel").childCount - 1;
    }
    
    public void RemoveItem(ConsumableItem itemEvent, int itemId) 
    {
        RemoveItemIconFromPanel(itemId);
        ConsumableVisualSetActive(itemId, false);
        Slot[itemId].clicked -= itemEvent.Use;
        PanelWithItems.Remove(itemId);
    }

    public void AddItem(ConsumableItem item)
    {
        int slotId = Finder.FindEmptySlot(SlotCount, PanelWithItems);
        if (slotId < 0)
            return;

        int identicalConsumableId = Finder.FindIdenticalConsumableItems(PanelWithItems, item);
        if (identicalConsumableId != -1)
        {
            ConsumableItem consumableItem = (ConsumableItem)PanelWithItems[identicalConsumableId];
            consumableItem.Increase(item.GetAmount());
            consumableItem.Refresh();
            return;
        }
        
        item.SetID(slotId);
        WhatIsItemType(slotId, item);
        AddItemIconToPanel(slotId, item);
        RegisterClickEventFromSlot(item, slotId);
        item.Initialize();
    }

    public void AddItem(NonConsumableItem item)
    {
        int slotId = Finder.FindEmptySlot(SlotCount, PanelWithItems);
        if (slotId < 0)
            return;

        item.SetID(slotId);
        WhatIsItemType(slotId, item);
        AddItemIconToPanel(slotId, item);
        RegisterClickEventFromSlot(item, slotId);
    }
    
    private void RegisterClickEventFromSlot(IItemUsable item, int slotId)
    {
        Slot[slotId].clicked += item.Use;
    }

    private void WhatIsItemType(int slotId, Item item)
    {
        if (item.TryGetComponent<ConsumableItem>(out var consumable))
        {
            RegisterConsumableSlot(slotId);
            ConsumableVisualSetActive(slotId, true);
        }
        else if (item.TryGetComponent<NonConsumableItem>(out var nonConsumable))
        {
            RegisterNonConsumableSlot(slotId);
        }
    }
    
    private void ConsumableVisualSetActive(int itemId, bool active)
    {
        ConsumableVisual[itemId].visible = active;
    }

    private void AddItemIconToPanel(int slotId, Item item)
    {
        PanelWithItems.Add(slotId, item);
        Slot[slotId].style.backgroundImage = GetItemIcon(slotId);
    }

    private void RemoveItemIconFromPanel(int itemId)
    {
        Slot[itemId].style.backgroundImage = RemoveItemIcon();
    }
    
    private void RegisterConsumableSlot(int slotId)
    {
        Slot[slotId] = Root.Q<Button>("Slot" + slotId);
        ConsumableVisual[slotId] = Root.Q<VisualElement>("Amount" + slotId);
    }
    
    private void RegisterNonConsumableSlot(int slotId)
    {
        Slot[slotId] = Root.Q<Button>("Slot" + slotId);
    }
    
    private StyleBackground GetItemIcon(int itemId)
    {
        return new StyleBackground(PanelWithItems[itemId].Icon());
    }

    private StyleBackground RemoveItemIcon()
    {
        Sprite sprite = null;
        return new StyleBackground(sprite);
    }
}