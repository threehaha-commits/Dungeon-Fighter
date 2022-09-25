using UnityEngine;

public class FuryStrike : MonoBehaviour, IAbilityTarget, ISetterNewEffect<IGetterPeriodicDamageable>
{
    public AbilityInfo InfoAbility { get; set; }
    private ParticleSystem Effect;
    [SerializeField] private float DamagePercent;
    public Transform Target { get; set; }
    public FuryStrikeLogic Ability { get; private set; }
    private IGetterPeriodicDamageable GetterPeriodicDamageable;
    
    private void Awake()
    {
        InfoAbility = Resources.Load<AbilityInfo>("Ability Object/Fury Strike");
        Effect = (ParticleSystem)InfoAbility.Effects.GetEffect(InfoAbility.Effects.Effect[0], transform);
        Ability = new FuryStrikeLogic(Effect, DamagePercent);
    }

    void IAbility.Use()
    {
        if (InfoAbility.AbilityMain.AbilityIsReady(Target, transform))
        {
            Ability.Use(Target);
            GetterPeriodicDamageable?.StartPeriodicDamage();
        }
    }

    void ISetterNewEffect<IGetterPeriodicDamageable>.SetNewEffect(IGetterPeriodicDamageable upgradeAbility)
    {
        GetterPeriodicDamageable = upgradeAbility;
    }
}