using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Document Document;
    [SerializeField] private Player Player;
    [SerializeField] private Transform SpawnPlayerPoint;
    [SerializeField] private ItemPanel Keeper;
    
    public override void InstallBindings()
    {
        BindDocument();
        BindTargetHandler();
        Container.Bind<ItemPanel>().FromInstance(Keeper).AsSingle();
        Container.Bind<DropWindow>().AsSingle().NonLazy();
        BindPlayer();
    }
    
    private void BindDocument()
    {
        Container.Bind<Document>().FromInstance(Document).AsSingle().Lazy();
    }
    
    private void BindTargetHandler()
    {
        Container.Bind<TargetHandler>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
    
    private void BindPlayer()
    {
        var player = Container.InstantiatePrefabForComponent<Player>
            (Player, SpawnPlayerPoint.position, Quaternion.identity, null);

        Container.Bind<Player>().FromInstance(player).AsSingle().NonLazy();
    }
}