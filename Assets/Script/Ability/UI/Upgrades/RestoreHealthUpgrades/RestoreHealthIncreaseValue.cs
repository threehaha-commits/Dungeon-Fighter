public class RestoreHealthIncreaseValue : IUpgradableEffect
{
    private readonly string Description = $"Увеличивает количество восстанавливаемого здоровья на {Value} ед. за каждое улучшение";
    private const float Value = 10;
    private readonly IValueChanger<RestoreHealthIncreaseValue, float> TargetInterface;
    
    public RestoreHealthIncreaseValue(IValueChanger<RestoreHealthIncreaseValue, float> targetInterface,
        DescriptionSkill description)
    {
        TargetInterface = targetInterface;
        description.Set(Description);
    }

    void IUpgradableEffect.Upgrade()
    {
        TargetInterface.ChangeValue(Value);
    }
}