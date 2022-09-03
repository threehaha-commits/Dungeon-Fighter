using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DropWindowFill
{
    private readonly DropWindowClickEvent ClickEvent;
    private List<Sprite> Sprites;
    private List<Item> DropItem;
    private UIDocument Document;
    
    public DropWindowFill(UIDocument document)
    {
        ClickEvent = new DropWindowClickEvent();
        Document = document;
    }
    
    public void Fill(IItemFactory factory, VisualElement dropList, ListView list, List<Item> dropItem, List<Sprite> sprites)
    {
        dropList.Clear();
        Sprites = sprites;
        DropItem = dropItem;
        list = new ListView(dropItem, 75, MakeItem, BindItem);
        dropList.Add(list);
        ClickEvent.AddEvent(factory, list, Sprites);
    }
    
    private VisualElement MakeItem()
    {
        var listItem = Resources.Load<VisualTreeAsset>("Cell");
        return listItem.Instantiate();
    }

    private void BindItem(VisualElement e, int index)
    {
        var button = e.Q<Button>();
        var label = e.Q<Label>();
        button.style.backgroundImage = new StyleBackground(Sprites[index]);
        label.text = DropItem[index].GetNameInfo();
        ClickEvent.AddEvent(index, button, DropItem, Document);
    }
}