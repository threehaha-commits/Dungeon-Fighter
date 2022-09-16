using UnityEngine;

public class FuryStrike : MonoBehaviour, IAbilityTarget
{
    public AbilityInfo InfoAbility { get; set; }
    private ParticleSystem Effect;
    [SerializeField] private float DamagePercent;
    public Transform Target { get; set; }
    private FuryStrikeLogic Ability;
    
    private void Awake()
    {
        InfoAbility = Resources.Load<AbilityInfo>("Ability Object/Fury Strike");
        Effect = (ParticleSystem)InfoAbility.Effects.GetEffect(InfoAbility.Effects.Effect[0], transform);
        Ability = new FuryStrikeLogic(Effect, DamagePercent);
    }

    void IAbility.Use()
    {
        if (InfoAbility.AbilityMain.AbilityIsReady(Target, transform))
            Ability.Use(Target);
    }
}