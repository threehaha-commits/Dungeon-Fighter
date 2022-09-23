using UnityEngine;

public class ChainHook : MonoBehaviour, IAbilityTarget, ISetterNewEffect<IProlongingEffect>, ISetterNewEffect<IGetterDamageable>
{
    public AbilityInfo InfoAbility { get; set; }
    private ParticleSystem Effect;
    private ChainBehaviour Chain;
    [SerializeField] private float HookSpeed;
    public Transform Target { get; set; }
    public ChainHookLogic Ability { get; private set; }
    private IProlongingEffect ProlongingEffect;
    private IGetterDamageable GetterDamageable;
    
    private void Awake()
    {
        InfoAbility = Resources.Load<AbilityInfo>("Ability Object/Chain Hook");
        Effect = (ParticleSystem)InfoAbility.Effects.GetEffect(InfoAbility.Effects.Effect[0], transform);
        Chain = InfoAbility.Effects.GetEffect<ChainBehaviour>(InfoAbility.Effects.Line[0], transform);
        Ability = new ChainHookLogic(this, InfoAbility, transform, HookSpeed, Effect, Chain);
    }

    void IAbility.Use()
    {
        if (InfoAbility.AbilityMain.AbilityIsReady(Target, transform))
        {
            Ability.Use(Target);
            ProlongingEffect?.ProlongingEffect();
            GetterDamageable?.ApplyDamage();
        }
    }

    void ISetterNewEffect<IProlongingEffect>.SetNewEffect(IProlongingEffect upgradeAbility)
    {
        ProlongingEffect = upgradeAbility;
    }

    void ISetterNewEffect<IGetterDamageable>.SetNewEffect(IGetterDamageable upgradeAbility)
    {
        GetterDamageable = upgradeAbility;
    }
}