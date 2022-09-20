public class HealthRegeneration : Regeneration
{
    private Health _Health;

    protected override void Start()
    {
        _Health = GetComponent<Health>();
        base.Start();
    }

    protected override void PointRegeneration(float value)
    {
        _Health.ChangeHpValue(value);
    }
}
