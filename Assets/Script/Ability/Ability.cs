using UnityEngine;

public interface IAbility
{
    AbilityInfo InfoAbility { get; set; }
    void Use();
}

public interface IAbilityTarget : IAbility
{
    Transform Target { set; }
}

public class Ability
{
    private readonly Reloader Reload;
    private readonly AbilityInfo Info;
    
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
        Info.AbilityVisualReload.StartVisualReload(Info.Cooldown);
        Reload.StartReload();
        return true;
    }
    
    public bool AbilityIsReady()
    {
        if (IsReload() == false)
            return false;
        Reload.StartReload();
        Info.AbilityVisualReload.StartVisualReload(Info.Cooldown);
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