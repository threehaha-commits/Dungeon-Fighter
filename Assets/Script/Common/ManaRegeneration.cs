public class ManaRegeneration : Regeneration
{
    private Mana _Mana;

    protected override void Start()
    {
        _Mana = GetComponent<Mana>();
        base.Start();
    }

    protected override void PointRegeneration(float value)
    {
        _Mana.ChangeManaValue(value);
    }
}
