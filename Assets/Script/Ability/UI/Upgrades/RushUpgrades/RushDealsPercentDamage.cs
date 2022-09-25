public class RushDealsPercentDamage : IUpgradableEffect
{
    private string Description = $"Способность наносит урон равный {DamagePercentPerLvl}% от макс хп противника за каждое улучшение";
    private const float DamagePercentPerLvl = 1.15f;
    private readonly IActioner<float> TargetInterface;
    private readonly ISetterNewEffect<IActioner<float>> UpgradableInterface;
    
    public RushDealsPercentDamage(IActioner<float> targetInterface,
        ISetterNewEffect<IActioner<float>> upgradableInterface, DescriptionSkill description)
    {
        TargetInterface = targetInterface;
        UpgradableInterface = upgradableInterface;
        description.Set(Description);
    }

    void IUpgradableEffect.Upgrade()
    {
        UpgradableInterface.SetNewEffect(TargetInterface);
        TargetInterface.Value += DamagePercentPerLvl;
    }
}