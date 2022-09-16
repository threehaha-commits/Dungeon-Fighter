using UnityEngine;

public class ChainHook : MonoBehaviour, IAbilityTarget
{
    public AbilityInfo InfoAbility { get; set; }
    private ParticleSystem Effect;
    private ChainBehaviour Chain;
    [SerializeField] private float HookSpeed;
    private const float StunDuration = 1.25f;
    public Transform Target { get; set; }
    private ChainHookLogic Ability;
    
    private void Awake()
    {
        InfoAbility = Resources.Load<AbilityInfo>("Ability Object/Chain Hook");
        Effect = (ParticleSystem)InfoAbility.Effects.GetEffect(InfoAbility.Effects.Effect[0], transform);
        Chain = InfoAbility.Effects.GetEffect<ChainBehaviour>(InfoAbility.Effects.Line[0], transform);
        Ability = new ChainHookLogic(this, InfoAbility, StunDuration, transform, HookSpeed, Effect, Chain);
    }

    void IAbility.Use()
    {
        if (InfoAbility.AbilityMain.AbilityIsReady(Target, transform))
            Ability.Use(Target);
    }
}