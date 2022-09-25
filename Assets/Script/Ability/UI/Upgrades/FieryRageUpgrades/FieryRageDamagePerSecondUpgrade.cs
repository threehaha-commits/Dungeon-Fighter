public class FieryRageDamagePerSecondUpgrade : IUpgradableEffect
{
    private string Description =
        $"Увеличивает тик переодического урона на {Value} ед. за каждое улучшение";
    private const float Value = 0.5f;
    private readonly IValueChanger<FieryRageDamagePerSecondUpgrade, float> TargetInterface;
    
    public FieryRageDamagePerSecondUpgrade(IValueChanger<FieryRageDamagePerSecondUpgrade, float> targetInterface, DescriptionSkill description)
    {
        TargetInterface = targetInterface;
        description.Set(Description);
    }

    void IUpgradableEffect.Upgrade()
    {
        TargetInterface.ChangeValue(Value);
    }
}