using System.Collections;
using UnityEngine;

public enum State {Move, NotMove }

public interface IStateHandler
{
    State StateHandler();
}

public interface IPeriodicDamageable
{
    MonoBehaviour Mono { get; set; }
    void StartPeriodicDamage(float duration, float damagePerSecond, ParticleSystem effect);
    IEnumerator PeriodicDamage(float duration, float damagePerSecond, ParticleSystem effect);
}

interface IStun : IStateHandler
{
    MonoBehaviour Mono { get; set; }
    void Stun(float duration);
}

public class CharapterState : MonoBehaviour
{
    [SerializeField] private Transform Head;
    [SerializeField] private ParticleSystem Stun;
    private State CurrentState;
    private IStun StunHandler;
    private PeriodicDamageHandler PeriodicHandler;
    private IMovable Move;
    
    private void Start()
    {
        Move = GetComponent<MoveHandler>();
        StunHandler = new StunHandler(Stun, this);
    }

    public void StartBurning(float duration, float damagePerSecond, ParticleSystem effect)
    {
        PeriodicHandler = new PeriodicDamageHandler(this, transform);
        IPeriodicDamageable periodicDamage = PeriodicHandler;
        periodicDamage.StartPeriodicDamage(duration, damagePerSecond, effect);
    }

    public void SetStun(float duration)
    {
        Debug.Log($"Stun duration is {duration}");
        StunHandler.Stun(duration);
        StateHandler(StunHandler);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            SetStun(3.5f);
    }

    public void StateHandler(IStateHandler handler)
    {
        CurrentState = handler.StateHandler();
        switch (CurrentState)
        {
            case State.Move:
                Move.Move(Move.Direction);
                break;
            case State.NotMove:
                Move.Move(Vector2.zero);
                break;
        }
    }

    private void FixedUpdate()
    {
        StateHandler(StunHandler);
    }
}