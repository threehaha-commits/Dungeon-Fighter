using UnityEngine;

public class Rush : MonoBehaviour, IAbilityTarget, ISetterNewEffect<IActioner<float>>
{
    public AbilityInfo InfoAbility { get; set; }
    private TrailRenderer Effect;
    [SerializeField] private float RushSpeed = 3f;
    private float StunDuration = 2.75f;
    public Transform Target { get; set; }
    public RushLogic Ability { get; private set; }
    private IActioner<float> GetterDamageable;
    
    private void Awake()
    {
        InfoAbility = Resources.Load<AbilityInfo>("Ability Object/Rush");
        Effect = (TrailRenderer)InfoAbility.Effects.GetEffect(InfoAbility.Effects.Trail[0], transform);
        Ability = new RushLogic(this, InfoAbility, transform, Effect, RushSpeed, StunDuration);
    }
    
    void IAbility.Use()
    {
        if (InfoAbility.AbilityMain.AbilityIsReady(Target, transform))
        {
            Ability.Use(Target);
            GetterDamageable?.Action();
        }
    }

    void ISetterNewEffect<IActioner<float>>.SetNewEffect(IActioner<float> upgradeAbility)
    {
        GetterDamageable = upgradeAbility;
    }
}