public class RestoreHealthIncreaseMaxHealth : IUpgradableEffect
{
    private readonly string Description = $"На {DurationValue} сек. увеличивает максимальное количество здоровья на {MaxValue} ед. за каждое улучшение";
    private const float MaxValue = 5f;
    private const float DurationValue = 5.5f;
    private readonly IActioner<RestoreHealthIncreaseMaxHealth, float> TargetInterface;
    private readonly ISetterNewEffect<IActioner<RestoreHealthIncreaseMaxHealth, float>> UpgradableInterface;
    
    public RestoreHealthIncreaseMaxHealth(IActioner<RestoreHealthIncreaseMaxHealth, float> targetInterface,
        ISetterNewEffect<IActioner<RestoreHealthIncreaseMaxHealth, float>> upgradableInterface, DescriptionSkill description)
    {
        TargetInterface = targetInterface;
        UpgradableInterface = upgradableInterface;
        description.Set(Description);
    }

    void IUpgradableEffect.Upgrade()
    {
        UpgradableInterface.SetNewEffect(TargetInterface);
        TargetInterface.Value += MaxValue;
    }
}