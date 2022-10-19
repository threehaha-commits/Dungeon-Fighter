using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class DropWindowClickEvent
{
    private IItemFactory Factory;
    private ListView List;
    private List<Sprite> Sprites;
    
    public void AddEvent(IItemFactory factory, ListView list, List<Sprite> sprites)
    {
        Factory = factory;
        List = list;
        Sprites = sprites;
        List.onSelectionChange += Click;
    }

    public void AddEvent(int index, Button button, List<Item> dropItem, UIDocument document)
    {
        DropWindowItemDescription itemDescription = new DropWindowItemDescription(document);
        itemDescription.SetDescription(button, dropItem[index]);
    }
    
    private void Click(IEnumerable<object> items)
    {
        Item selectionItem = (Item)items.First();
        Factory.Create(selectionItem);
        List.itemsSource.Remove(selectionItem);
        Sprites.Remove(selectionItem.Icon());
        List.Rebuild();
    }
}