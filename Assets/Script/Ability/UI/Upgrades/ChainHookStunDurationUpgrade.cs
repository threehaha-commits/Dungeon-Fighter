public class ChainHookStunDurationUpgrade : IUpgradableEffect
{
    private const float UpgradeDurationPerLvl = 0.35f;
    private readonly IProlongingEffect TargetInterface;
    private readonly ISetterNewEffect<IProlongingEffect> UpgradableInterface;
    
    public ChainHookStunDurationUpgrade(IProlongingEffect targetInterface, ISetterNewEffect<IProlongingEffect> upgradableInterface)
    {
        TargetInterface = targetInterface;
        UpgradableInterface = upgradableInterface;
    }

    void IUpgradableEffect.Upgrade()
    {
        UpgradableInterface.SetNewEffect(TargetInterface);
        TargetInterface.ProlongDuration += UpgradeDurationPerLvl;
    }
}