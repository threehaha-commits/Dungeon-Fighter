using UnityEngine.UIElements;
using Zenject;

public class PlayerMana : Mana
{
    private UIDocument Document;
    private ProgressBar ManaBar;

    [Inject]
    private void Construct(Document document)
    {
        Document = document.Bar;
        ManaChanger = new BarVisualChanger(Document, "ManaBar");
    }

    protected override void Start()
    {
        base.Start();
        ManaChanger.ChangeBar(CurrentMana, MaxMana);
    }

    public override void ChangeManaValue(float Value)
    {
        base.ChangeManaValue(Value);
        ManaChanger.ChangeBar(CurrentMana, MaxMana);
    }

    public void ChangeMaxManaValue(float value)
    {
        MaxMana += value;
        CurrentMana = MaxMana;
        ManaChanger.ChangeBar(CurrentMana, MaxMana);
    }
}