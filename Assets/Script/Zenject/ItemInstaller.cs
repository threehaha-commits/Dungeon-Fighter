using System.Collections;
using UnityEngine;
using Zenject;

public class ItemInstaller : MonoInstaller, IInitializable
{

    public override void InstallBindings()
    {
        BindInterfaceInstaller();
        BindDropBoxFactory();
        BindItemFactory();
    }

    private void BindInterfaceInstaller()
    {
        Container.BindInterfacesTo<ItemInstaller>().FromInstance(this).AsSingle();
    }

    private void BindDropBoxFactory()
    {
        Container.Bind<IDropBoxFactory>().To<DropBoxFactory>().AsSingle();
    }

    private void BindItemFactory()
    {
        Container.Bind<IItemFactory>().To<ItemFactory>().AsSingle();
    }

    public void Initialize()
    {
        var dropBoxFactory = Container.Resolve<IDropBoxFactory>();
        Container.Bind<IDropBoxFactory>().FromInstance(dropBoxFactory).AsSingle().NonLazy();
        
        var itemFactory = Container.Resolve<IItemFactory>();
        Container.Bind<IItemFactory>().FromInstance(itemFactory).AsSingle().NonLazy();
    }
}

