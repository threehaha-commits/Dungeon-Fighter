using System.ComponentModel;
using UnityEngine;
using Zenject;

public class AbilityInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindAbilities();
        BindAbilitiesTarget();
    }
    
    private void BindAbilities()
    {
        Container.Bind<IAbility>().FromComponentsInHierarchy().AsSingle();
    }

    private void BindAbilitiesTarget()
    {
        Container.Bind<IAbilityTarget>().FromComponentsInHierarchy().AsSingle();
    }
}