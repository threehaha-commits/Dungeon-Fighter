﻿using UnityEngine;
using Zenject;

public interface IItemFactory
{
    Item Create(Item item);
}

public class ItemFactory : IItemFactory
{
    private readonly DiContainer container;
    [Inject] private Player PlayerCharacter;
    [Inject] private ItemPanel Panel;
    [Inject] private Document Document;
    
    public ItemFactory(DiContainer container)
    {
        this.container = container;
    }

    public Item Create(Item item)
    {
        var itemFromResource = Resources.Load<Item>("Items/Potion/" + item.GetIcon().name);
        var newItem = container.InstantiatePrefabForComponent<Item>(itemFromResource);
        newItem.transform.parent = PlayerCharacter.transform;
        ConsumableItem consumableItem = (ConsumableItem)newItem;
        consumableItem.Construct(new ConsumableHandler(Panel), new ConsumableVisual(Document), Panel);
        return newItem;
    }
}