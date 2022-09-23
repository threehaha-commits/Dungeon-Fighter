public interface IUpgradeAbility { }

public interface IUpgradableEffect
{
    void Upgrade();
}

public interface ISetterNewEffect<T> where T : IUpgradeAbility
{
    void SetNewEffect(T upgradeAbility);
}

public interface IProlongingEffect : IUpgradeAbility
{
    float ProlongDuration { get; set; }
    void ProlongingEffect();
}

public interface IGetterDamageable : IUpgradeAbility
{
    float Damage { get; set; }
    void ApplyDamage();
}

public interface IReducibleCooldown : IUpgradeAbility
{
    void ReduceCooldown(float reduceCooldownTime);
}