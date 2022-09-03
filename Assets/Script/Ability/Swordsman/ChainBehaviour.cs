using System.Collections;
using UnityEngine;

public class ChainBehaviour : MonoBehaviour
{
    private LineRenderer LineRenderer;
    
    private void Awake()
    {
        LineRenderer = GetComponent<LineRenderer>();
    }

    public void Active(Transform target, Transform player, float minDistance, ParticleSystem effect) 
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
        (Transform target, Transform player, float minDistance, ParticleSystem effect, float distance = 1f)
    {
        while (distance > minDistance && (target != null && player != null))
        {
            LineRenderer.SetPosition(0, player.position);
            LineRenderer.SetPosition(1, target.position);
            effect.transform.position = target.position;
            distance = (target.position - player.position).magnitude;
            yield return new WaitForFixedUpdate();
        }
        Deactive(effect);
    }

    private void Deactive(ParticleSystem effect)
    {
        effect.Stop();
        gameObject.SetActive(false);
    }
}
