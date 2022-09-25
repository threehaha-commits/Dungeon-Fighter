using UnityEngine;

public interface IUpgradeAbility { }

public interface IUpgradableEffect
{
    void Upgrade();
}

public interface ISetterNewEffect<T> where T : IUpgradeAbility
{
    void SetNewEffect(T upgradeAbility);
}

/// <typeparam name="T">Type for identification identical interfaces</typeparam>
public interface IValueChanger<T, K>
{
    void ChangeValue(K value);
}

public interface IValueChanger<T>
{
    void ChangeValue(T value);
}

/// <typeparam name="T">Type for identification identical interfaces</typeparam>
public interface IActioner<T, K> : IUpgradeAbility
{
    K Value { get; set; }
    void Action();
}

public interface IActioner<T> : IUpgradeAbility
{
    T Value { get; set; }
    void Action();
}

public interface IGetterPeriodicDamageable : IUpgradeAbility
{
    float Duration { get; set; }
    float DamagePerSecond { get; set; }
    ParticleSystem Effect { get; set; }
    void StartPeriodicDamage();
}