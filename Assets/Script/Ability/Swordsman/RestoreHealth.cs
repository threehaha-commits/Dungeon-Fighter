using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RestoreHealth : MonoBehaviour, IAbility
{
    [SerializeField] private AbilityInfo Info;
    public AbilityInfo InfoAbility => Info;
    private ParticleSystem Effect;
    [SerializeField] private float LifeRestore;
    private Health _Health;

    private void Awake()
    {
        Effect = (ParticleSystem)Info.Effects.GetEffect(Info.Effects.Effect[0], transform);
    }

    private void Start()
    {
        _Health = GetComponent<Health>();
    }
    
    void IAbility.Use()
    {
        if (!Info.AbilityMain.AbilityIsReady())
            return;
        Effect.Play();
        _Health.Restore(LifeRestore);
    }
}
