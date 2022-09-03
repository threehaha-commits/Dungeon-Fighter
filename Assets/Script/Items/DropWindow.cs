using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class DropWindow
{
    private static List<Sprite> Sprites;
    private static IItemFactory Factory;
    private static ListView List;
    private static VisualElement DropList;
    private static DropWindowFill FillDrop;
    private static UIDocument Document;
    
    public DropWindow(IItemFactory factory)
    {
        Factory = factory;
    }

    [Inject]
    private void Construct(Document document)
    {
        Document = document.DescriptionWindow;;
        MouseOverUI.AddElement(document.DescriptionWindow, "DropWindow");
        FillDrop = new DropWindowFill(Document);
    }
    
    public static void Create(Item[] dropItems)
    {
        var root = Document.rootVisualElement;
        DropList = root.Q<VisualElement>("DropList");
        Sprites = new List<Sprite>();

        FillSprites(dropItems);
        
        FillDrop.Fill(Factory, DropList, List, dropItems.ToList(), Sprites);
    }

    private static void FillSprites(Item[] dropItems)
    {
        for (int i = 0; i < dropItems.Length; i++)
            Sprites.Add(dropItems[i].GetIcon());
    }
}