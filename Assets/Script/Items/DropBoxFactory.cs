using UnityEngine;
using Zenject;

public interface IDropBoxFactory
{
    void Create(Item[] item, Vector2 position);
}

public class DropBoxFactory : IDropBoxFactory
{
    [Inject] private Document Document;
    private readonly DiContainer container;
    private Chest DropBox;

    public DropBoxFactory(DiContainer container)
    {
        this.container = container;
        DropBox = Resources.Load<Chest>("Chest");
    }

    void IDropBoxFactory.Create(Item[] item, Vector2 position)
    {
        var dropBox = container.InstantiatePrefabForComponent<Chest>(DropBox);
        dropBox.SetItems(item);
    }
}