public class ChainHookDealsDamage : IUpgradableEffect
{
    private const float UpgradeDamagePerLvl = 2.5f;
    private readonly IGetterDamageable TargetInterface;
    private readonly ISetterNewEffect<IGetterDamageable> UpgradableInterface;

    public ChainHookDealsDamage(IGetterDamageable targetInterface, ISetterNewEffect<IGetterDamageable> upgradableInterface)
    {
        TargetInterface = targetInterface;
        UpgradableInterface = upgradableInterface;
    }
    
    void IUpgradableEffect.Upgrade()
    {
        UpgradableInterface.SetNewEffect(TargetInterface);
        TargetInterface.Damage += UpgradeDamagePerLvl;
    }
}