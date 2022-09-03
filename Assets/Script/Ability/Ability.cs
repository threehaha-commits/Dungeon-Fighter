using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public interface IAbility
{
    AbilityInfo InfoAbility { get; }
    void Use();
}

public interface IAbilityTarget : IAbility
{
    Transform Target { get; set; }
}

public class Ability
{
    private float CurrentCooldown = 0f;
    private Reloader Reload;
    private AbilityInfo Info;
    
    public Ability(AbilityInfo info)
    {
        Info = info;
        Reload = new Reloader(info.Cooldown);
    }
    
    public bool AbilityIsReady(Transform target, Transform transform)
    {
        if (IsReload() == false)
            return false;
        if (target == null)
            return false;
        if (IsDistance(target, transform) == false)
            return false;
        Reload.StartReload();
        return true;
    }
    
    public bool AbilityIsReady()
    {
        if (IsReload() == false)
            return false;
        Reload.StartReload();
        return true;
    }

    private bool IsReload()
    {
        return Reload.Reloaded;
    }

    private bool IsDistance(Transform target, Transform transform)
    {
        float distance = (target.position - transform.position).sqrMagnitude;
        return distance > Info.MinRange && distance < Info.MaxRange;
    }
}