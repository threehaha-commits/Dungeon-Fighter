using UnityEngine.UIElements;
using Zenject;

public class PlayerMana : Mana
{
    private UIDocument Document;
    private ProgressBar ManaBar;

    [Inject]
    private void Construct(Document Document)
    {
        this.Document = Document.Bar;
    }

    protected override void Start()
    {
        base.Start();
        var root = Document.rootVisualElement;
        ManaBar = root.Q<ProgressBar>("ManaBar");
        ManaBar.value = ChangeManaBar();
    }

    public override void Restore(float Value)
    {
        base.Restore(Value);
        ManaBar.value = ChangeManaBar();
    }

    private float ChangeManaBar()
    {
        ManaBar.title = $"{CurrentMana}/{MaxMana}";
        return ManaBar.highValue * CurrentMana / MaxMana;
    }
}
