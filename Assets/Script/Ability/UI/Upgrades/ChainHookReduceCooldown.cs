public class ChainHookReduceCooldown : IUpgradableEffect
{
    private const float UpgradeReduceCooldown = 0.4f;
    private readonly IReducibleCooldown TargetInterface;
    
    public ChainHookReduceCooldown(IReducibleCooldown targetInterface)
    {
        TargetInterface = targetInterface;
    }

    void IUpgradableEffect.Upgrade()
    {
        TargetInterface.ReduceCooldown(UpgradeReduceCooldown);
    }
}