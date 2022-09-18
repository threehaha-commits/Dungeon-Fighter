using System;
using System.Collections;
using UnityEngine;

public class ChainBehaviour : MonoBehaviour
{
    private LineRenderer LineRenderer;
    
    private void Awake()
    {
        LineRenderer = GetComponent<LineRenderer>();
    }

    public void Activate(Transform target, Transform player, float minDistance, ParticleSystem effect) 
    {
        if (target == null)
        {
            Debug.LogError("ChainBehaviour - Target is null!");
            return;
        }
        gameObject.SetActive(true);
        effect.Play();
        StartCoroutine(Drag(target, player, minDistance, effect));
    }

    private IEnumerator Drag
        (Transform target, Transform player, float minDistance, ParticleSystem effect)
    {
        var distance = 1f;
        while (distance > minDistance && (target != null && player != null))
        {
            LineRenderer.SetPosition(0, player.position);
            LineRenderer.SetPosition(1, target.position);
            effect.transform.position = target.position;
            distance = (target.position - player.position).sqrMagnitude;
            yield return new WaitForFixedUpdate();
        }
        Deactivate(effect);
    }

    private void Deactivate(ParticleSystem effect)
    {
        effect.Stop();
        gameObject.SetActive(false);
    }
}