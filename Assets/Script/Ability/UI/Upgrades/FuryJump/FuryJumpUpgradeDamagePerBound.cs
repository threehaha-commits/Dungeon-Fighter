public class FuryJumpUpgradeDamagePerBound : IUpgradableEffect
{
    private readonly string Description = $"Увеличивает урон каждого прыжка на {UpgradeDamagePerLvl} ед. за каждое улучшение";
    private const float UpgradeDamagePerLvl = 0.55f;
    private readonly IValueChanger<FuryJumpUpgradeDamagePerBound, float> TargetInterface;
    
    public FuryJumpUpgradeDamagePerBound(IValueChanger<FuryJumpUpgradeDamagePerBound, float> targetInterface,
        DescriptionSkill description)
    {
        TargetInterface = targetInterface;
        description.Set(Description);
    }

    void IUpgradableEffect.Upgrade()
    {
        TargetInterface.ChangeValue(UpgradeDamagePerLvl);
    }
}