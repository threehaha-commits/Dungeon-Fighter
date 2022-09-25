using UnityEngine;

public class FuryStrikeGetPeriodicDamage : IUpgradableEffect
{
    private readonly  string Description;
    private const float UpgradeDamagePerLvl = 1.5f; 
    private readonly IGetterPeriodicDamageable TargetInterface;
    private readonly ISetterNewEffect<IGetterPeriodicDamageable> UpgradableInterface;
    
    public FuryStrikeGetPeriodicDamage(IGetterPeriodicDamageable targetInterface, ISetterNewEffect<IGetterPeriodicDamageable> upgradableInterface, DescriptionSkill description)
    {
        TargetInterface = targetInterface;
        UpgradableInterface = upgradableInterface;
        var effectName = "BloodEffectFromFuryStrike";
        var effectObject = Resources.Load<ParticleSystem>("Ability Effects/Swordsman/" + effectName);
        TargetInterface.Effect = Object.Instantiate(effectObject);
        Description = $"Добавляет способности кровотечение." +
                      $" Действует {TargetInterface.Duration} сек. и наносит {TargetInterface.DamagePerSecond} ед. урона раз в сек. + {UpgradeDamagePerLvl} ед. за каждое улучшение";
        description.Set(Description);
    }

    void IUpgradableEffect.Upgrade()
    {
        UpgradableInterface.SetNewEffect(TargetInterface);
        TargetInterface.DamagePerSecond += UpgradeDamagePerLvl;
    }
}