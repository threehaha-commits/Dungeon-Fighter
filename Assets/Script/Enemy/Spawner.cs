using System.Threading.Tasks;
using UnityEngine;

public class Spawner : MonoBehaviour, IDeathInspector
{
    [SerializeField] private float SpawnTime;
    private int SpawnTimeInMillisecond => Mathf.RoundToInt(SpawnTime * 1000);
    private Vector3 DefaultPosition;

    private void Awake()
    {
        DefaultPosition = transform.position;
    }

    void IDeathInspector.ApplyDamage(Health health)
    {
        if (health.IsDeath)
            StartSpawn(health);
    }

    private async void StartSpawn(Health health)
    {
        gameObject.SetActive(false);

        await Task.Delay(SpawnTimeInMillisecond);

        transform.position = DefaultPosition;
        health.Restore(health.GetMaxHealth());
        gameObject.SetActive(true);
    }
}
