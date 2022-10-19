using UnityEngine;

public class FuryJump : AbilityInputs, IAbilityTarget
{
    public AbilityInfo InfoAbility { get; set; }
    private TrailRenderer TrailEffect;
    private ParticleSystem BoundEffect;
    [SerializeField] private float DamagePerBound;
    [SerializeField] private int BoundCount;
    [SerializeField] private float TimeBetweenJump;
    private BoxCollider2D Collider;
    public Transform Target { get; set; }
    private CharapterState State;
    public FuryJumpLogic Ability { get; private set; }
    
    private void Awake()
    {
        InputAbility = this;
        InfoAbility = Resources.Load<AbilityInfo>("Ability Object/Fury Jump");
        TrailEffect = (TrailRenderer)InfoAbility.Effects.GetEffect(InfoAbility.Effects.Trail[0], transform);
        BoundEffect = (ParticleSystem)InfoAbility.Effects.GetEffect(InfoAbility.Effects.Effect[0], transform);
    }

    private void Start()
    {
        State = GetComponent<CharapterState>();
        Collider = transform.root.GetComponent<BoxCollider2D>();
        Ability = new FuryJumpLogic(this, transform, TrailEffect, BoundEffect, DamagePerBound,
            BoundCount, TimeBetweenJump, Collider, State);
    }

    void IAbility.Use()
    {
        if (InfoAbility.AbilityMain.AbilityIsReady(Target, transform))
            Ability.Use(Target);
    }
}