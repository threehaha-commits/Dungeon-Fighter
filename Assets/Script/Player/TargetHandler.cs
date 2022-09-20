using UnityEngine;
using Zenject;

public class TargetHandler : MonoBehaviour, IDeathInspector
{
    private Transform Target;
    [Inject] private IAbilityTarget[] AbilitiesTarget;
    private VisualTargetWindow TargetWindow;
    
    [Inject]
    private void Construct(Document Document)
    {
        TargetWindow = new VisualTargetWindow(Document);
    }
    
    public void SetTarget(Transform target)
    {
        Target = target;
        TargetWindow.OpenWithTarget(target);
        SendTargetToAbility();
    }

    private void SendTargetToAbility()
    {
        foreach (var target in AbilitiesTarget)
        {
            target.Target = Target;
        }    
    }
    
    public void ApplyDamage(Health health)
    {
        if (!health.IsDeath)
            return;
        
        Target = null;
        TargetWindow.Close();
    }
}