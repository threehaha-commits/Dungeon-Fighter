public class FuryJumpUpgradeCountBound : IUpgradableEffect
{
    private readonly string Description = "Увеличивает количество прыжков на 1 за каждое улучшение";
    private readonly IValueChanger<FuryJumpUpgradeCountBound, int> TargetInterface;

    public FuryJumpUpgradeCountBound(IValueChanger<FuryJumpUpgradeCountBound, int> targetInterface,
        DescriptionSkill description)
    {
        TargetInterface = targetInterface;
        description.Set(Description);
    }

    void IUpgradableEffect.Upgrade()
    {
        TargetInterface.ChangeValue(1);
    }
}