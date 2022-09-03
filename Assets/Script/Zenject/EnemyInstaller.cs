using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    [SerializeField] private Grid Grid; // Grid
    [SerializeField] private Tilemap Tile; // Ground
    
    public override void InstallBindings()
    {
        Container.Bind<EnemyMove>().AsSingle();
        Container.Bind<PathFinder>().FromInstance(new PathFinder(Grid, Tile)).AsSingle();
    }
}