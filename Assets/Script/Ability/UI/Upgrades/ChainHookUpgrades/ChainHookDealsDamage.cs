public class ChainHookDealsDamage : IUpgradableEffect
{
    private string Description =
        $"Добавляет способности возможность наносить урон, который равен {UpgradeDamagePerLvl} ед. за каждое улучшение";
    private const float UpgradeDamagePerLvl = 2.5f;
    private readonly IActioner<ChainHookDealsDamage, float> TargetInterface;
    private readonly ISetterNewEffect<IActioner<ChainHookDealsDamage, float>> UpgradableInterface;

    public ChainHookDealsDamage(IActioner<ChainHookDealsDamage, float> targetInterface, ISetterNewEffect<IActioner<ChainHookDealsDamage, float>> upgradableInterface, DescriptionSkill description)
    {
        TargetInterface = targetInterface;
        UpgradableInterface = upgradableInterface;
        description.Set(Description);
    }
    
    void IUpgradableEffect.Upgrade()
    {
        UpgradableInterface.SetNewEffect(TargetInterface);
        TargetInterface.Value += UpgradeDamagePerLvl;
    }
}