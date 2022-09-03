using System.Collections;
using UnityEngine;

public class StunHandler : IStun
{
    private ParticleSystem StunEffect;
    public MonoBehaviour Mono { get; set; }
    private float StunDuration;
    private bool IsStuning;

    public StunHandler(ParticleSystem stun, MonoBehaviour mono)
    {
        StunEffect = stun;
        Mono = mono;
    }
    
    public State StateHandler()
    {
        if (IsStuning)
            return State.NotMove;
        
        return State.Move;
    }

    void IStun.Stun(float duration)
    {
        if(IsStuning == false)
            Mono.StartCoroutine(MakeCharacterToStun(duration));
        else 
            StunDuration += duration;
    }

    private IEnumerator MakeCharacterToStun(float duration)
    {
        StunDuration = duration;
        IsStuning = true;
        StunEffect.Play();
        while (StunDuration > 0)
        {
            StunDuration--;
            yield return new WaitForSeconds(1f);
        }
        IsStuning = false;
        StunEffect.Stop();
    }
}