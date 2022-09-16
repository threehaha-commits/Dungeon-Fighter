using UnityEngine;

public class RestoreHealthLogic
{
    private readonly ParticleSystem Effect;
    private readonly float LifeRestore;
    private readonly Health Health;
    
    public RestoreHealthLogic(ParticleSystem effect, Health health, float lifeRestore)
    {
        Effect = effect;
        Health = health;
        LifeRestore = lifeRestore;
    }
    
    public void Use()
    {
        Effect.Play();
        Health.Restore(LifeRestore);
    }
}