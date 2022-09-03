using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHandler : MonoBehaviour
{
    private Dictionary<MonoBehaviour, IEnumerator> CoroutineDictionary = new Dictionary<MonoBehaviour, IEnumerator>();

    public void AddCoroutine(MonoBehaviour component, IEnumerator coroutine)
    {
        CoroutineDictionary.Add(component, coroutine);
    }

    private void OnBecameVisible()
    {
        foreach(var coroutine in CoroutineDictionary)
        {
            coroutine.Key.StartCoroutine(coroutine.Value);
        }
    }

    private void OnBecameInvisible()
    {
        foreach (var coroutine in CoroutineDictionary)
        {
            coroutine.Key.StopCoroutine(coroutine.Value);
        }
    }
}
