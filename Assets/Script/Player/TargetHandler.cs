using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class TargetHandler : MonoBehaviour, IDeathInspector
{
    public Transform Target { get; private set; }
    private VisualElement Window;
    private Label TargetName;
    private IChangeBarColor TargetBar;
    [Inject] private IAbilityTarget[] AbilitiesTarget;
    
    [Inject]
    private void Construct(Document Document)
    {
        var root = Document.Target.rootVisualElement;
        Window = root.Q<VisualElement>("Window");
        TargetName = root.Q<Label>("TargetName");
        Window.visible = false;
    }
    
    public void SetTarget(Transform target)
    {
        Target = target;
        Window.visible = true;
        TargetName.text = Target.name;
        ChangeColorBar(Target);
        SendTargetInAbility();
    }

    private void SendTargetInAbility()
    {
        foreach (var target in AbilitiesTarget)
        {
            target.Target = Target;
        }    
    }
    
    public void ApplyDamage(Health health)
    {
        if (health.IsDeath)
        {
            Target = null;
            Window.visible = false;
            TargetName.text = "null";
        }
    }

    private void ChangeColorBar(Transform Target)
    {
        IChangeBarColor targetBar = Target.GetComponentInChildren<IChangeBarColor>();
        if(TargetBar != null)
            TargetBar.ChangeColor(Color.red);

        TargetBar = targetBar;
        TargetBar.ChangeColor(Color.yellow);
    }
}
