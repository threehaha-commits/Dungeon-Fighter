using UnityEngine;
using UnityEngine.Events;

public class EnemyAbilitiesEvents
{
    private UnityAction<GameObject> TargetSetter;

    public EnemyAbilitiesEvents(GameObject enemy)
    {
        TargetSetter += enemy.GetComponent<EnemyAbilityController>().GetTarget;
        TargetSetter += enemy.GetComponent<EnemyMove>().SetPath;
    }

    public void InvokeAction(GameObject target)
    {
        TargetSetter?.Invoke(target);
    }

    public void Cancel(GameObject target)
    {
        TargetSetter?.Invoke(target);
    }
}