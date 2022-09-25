public class FuryStrikeIncreaseDamage : IUpgradableEffect
{
    private string Description =
        $"Увеличивает урон на {UpgradePercentDamagePerLvl}% за каждое улучшение";
    private const float UpgradePercentDamagePerLvl = 1f;
    private readonly IValueChanger<float> TargetInterface;

    public FuryStrikeIncreaseDamage(IValueChanger<float> targetInterface, DescriptionSkill description)
    {
        TargetInterface = targetInterface;
        description.Set(Description);
    }

    void IUpgradableEffect.Upgrade()
    {
        TargetInterface.ChangeValue(UpgradePercentDamagePerLvl);
    }
}