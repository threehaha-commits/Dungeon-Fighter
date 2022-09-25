public class FieryRageDurationUpgrade : IUpgradableEffect
{
    private string Description =
        $"Увеличивает время горения противника на {UpgradeDurationPerLvl} сек. за каждое улучшение";
    private const float UpgradeDurationPerLvl = 0.75f;
    private readonly IActioner<float> TargetInterface;
    
    public FieryRageDurationUpgrade(IActioner<float> targetInterface, DescriptionSkill description)
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