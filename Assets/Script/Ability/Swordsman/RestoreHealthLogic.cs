using UnityEditor.VersionControl;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

public class RestoreHealthLogic : IValueChanger<RestoreHealthIncreaseValue, float>, IActioner<RestoreHealthIncreaseMaxHealth, float>
{
    private readonly ParticleSystem Effect;
    private float LifeRestoreValue;
    private readonly Health Health;
    private readonly IActioner<RestoreHealthIncreaseMaxHealth, float> IncreaseMaxHealthValue;
    float IActioner<RestoreHealthIncreaseMaxHealth, float>.Value { get; set; }
    
    public RestoreHealthLogic(ParticleSystem effect, Health health, float lifeRestore)
    {
        Effect = effect;
        Health = health;
        LifeRestoreValue = lifeRestore;
        IncreaseMaxHealthValue = this;
    }
    
    public void Use()
    {
        Effect.Play();
        Health.ChangeHpValue(LifeRestoreValue);
    }

    void IValueChanger<RestoreHealthIncreaseValue, float>.ChangeValue(float value)
    {
        LifeRestoreValue += value;
    }

    async void IActioner<RestoreHealthIncreaseMaxHealth, float>.Action()
    {
        Health.ChangeMaxHealthValue(IncreaseMaxHealthValue.Value);
        await Task.Delay(5000);
        Health.ChangeMaxHealthValue(-IncreaseMaxHealthValue.Value);
    }
}