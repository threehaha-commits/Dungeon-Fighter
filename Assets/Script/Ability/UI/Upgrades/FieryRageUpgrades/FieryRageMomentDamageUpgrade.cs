public class FieryRageMomentDamageUpgrade : IUpgradableEffect
{
    private string Description =
        $"Увеличивает моментальный урон на {Value} ед. за каждое улучшение";
    private const float Value = 1.25f;
    private readonly IValueChanger<FieryRageMomentDamageUpgrade, float> TargetInterface;
    
    public FieryRageMomentDamageUpgrade(IValueChanger<FieryRageMomentDamageUpgrade, float> targetInterface, DescriptionSkill description)
    {
        TargetInterface = targetInterface;
        description.Set(Description);
    }

    void IUpgradableEffect.Upgrade()
    {
        TargetInterface.ChangeValue(Value);
    }
}