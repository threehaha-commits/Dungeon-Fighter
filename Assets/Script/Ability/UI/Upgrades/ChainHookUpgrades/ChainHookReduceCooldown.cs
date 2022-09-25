public class ChainHookReduceCooldown : IUpgradableEffect
{
    private string Description =
        $"Уменьшает время перезарядки способности на {UpgradeReduceCooldown} сек. за каждое улучшение";
    private const float UpgradeReduceCooldown = 0.4f;
    private readonly IValueChanger<float> TargetInterface;
    
    public ChainHookReduceCooldown(IValueChanger<float> targetInterface, DescriptionSkill description)
    {
        TargetInterface = targetInterface;
        description.Set(Description);
    }

    void IUpgradableEffect.Upgrade()
    {
        TargetInterface.ChangeValue(UpgradeReduceCooldown);
    }
}