using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Regeneration : MonoBehaviour
{
    [SerializeField] private float RegenerationPerSecond;

    protected virtual void Start()
    {
        StartCoroutine(GeneratorHandler());
    }

    private IEnumerator GeneratorHandler()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            PointRegeneration(RegenerationPerSecond);
        }
    }

    protected abstract void PointRegeneration(float value);
}
