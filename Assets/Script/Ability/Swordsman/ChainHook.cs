using System.Collections;
using UnityEngine;

public class ChainHook : MonoBehaviour, IAbilityTarget
{
    [SerializeField] private AbilityInfo Info;
    public AbilityInfo InfoAbility => Info;
    private ParticleSystem Effect;
    private ChainBehaviour Chain;
    [SerializeField] private float HookSpeed;
    private const float StunDuration = 1.25f;
    public Transform Target { get; set; }

    private void Awake()
    {
        Effect = (ParticleSystem)Info.Effects.GetEffect(Info.Effects.Effect[0], transform);
        Chain = Info.Effects.GetEffect<ChainBehaviour>(Info.Effects.Line[0], transform);
    }

    void IAbility.Use()
    {
        if (!Info.AbilityMain.AbilityIsReady(Target, transform))
            return;
        Chain.Active(Target, transform, Info.MinRange, Effect);
        Target.GetComponent<CharapterState>().SetStun(StunDuration);
        StartCoroutine(Hooking());
    }

    private IEnumerator Hooking(float distance = 1f)
    {
        while (distance > Info.MinRange && Target != null)
        {
            distance = (transform.position - Target.position).sqrMagnitude;
            Target.position = Vector3.MoveTowards(Target.position, transform.position,
                HookSpeed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
}
