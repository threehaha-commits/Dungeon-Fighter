public class ChainHookStunDurationUpgrade : IUpgradableEffect
{
    private string Description =
        $"Увеличивает время действия оглушения противника на {UpgradeDurationPerLvl} сек. за каждое улучшение";
    private const float UpgradeDurationPerLvl = 0.35f;
    private readonly IActioner<ChainHookStunDurationUpgrade, float> TargetInterface;
    
    public ChainHookStunDurationUpgrade(IActioner<ChainHookStunDurationUpgrade, float> targetInterface, DescriptionSkill description)
    {
        TargetInterface = targetInterface;
        TargetInterface.Value = UpgradeDurationPerLvl;
        description.Set(Description);
    }

    void IUpgradableEffect.Upgrade()
    {
        TargetInterface.Action();
    }
}