using UnityEngine;
using UnityEngine.Events;

public class EnemyStateEvents
{
    private readonly UnityAction<GameObject> TargetSetter;

    public EnemyStateEvents(GameObject enemy)
    {
        if(enemy.TryGetComponent(out EnemyAbilityController abilitiesController))
            TargetSetter += abilitiesController.GetTarget;
        TargetSetter += enemy.GetComponent<EnemyMove>().SetPath;
    }

    public void Invoke(GameObject target)
    {
        TargetSetter?.Invoke(target);
    }

    public void Cancel(GameObject target)
    {
        TargetSetter?.Invoke(target);
    }
}