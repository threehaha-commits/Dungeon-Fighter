using UnityEngine;

public class RestoreHealth : MonoBehaviour, IAbility
{
    public AbilityInfo InfoAbility { get; set; }
    private ParticleSystem Effect;
    [SerializeField] private float LifeRestore;
    private Health _Health;
    private RestoreHealthLogic Ability;
    
    private void Awake()
    {
        InfoAbility = Resources.Load<AbilityInfo>("Ability Object/Restore Health");
        Effect = (ParticleSystem)InfoAbility.Effects.GetEffect(InfoAbility.Effects.Effect[0], transform);
    }

    private void Start()
    {
        _Health = GetComponent<Health>();
        Ability = new RestoreHealthLogic(Effect, _Health, LifeRestore);
    }
    
    void IAbility.Use()
    {
        if (InfoAbility.AbilityMain.AbilityIsReady())
            Ability.Use();
    }
}