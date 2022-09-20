using UnityEngine;
using Zenject;

public class EnemyExpirienceSetter : MonoBehaviour, IDeathInspector
{
    [SerializeField] private int ExpirienceForDeath;
    [Inject] private IExpirience ExpirienceCharapter;

    void IDeathInspector.ApplyDamage(Health health)
    {
        if (!health.IsDeath)
            return;
        ExpirienceCharapter.SetExp(ExpirienceForDeath);
    }
}