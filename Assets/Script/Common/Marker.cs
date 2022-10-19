using UnityEngine;

public class Marker : MonoBehaviour
{
    public Color Color;
    [Range(0f, 1f)]
    public float Radius;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color;
        Gizmos.DrawSphere(transform.position, Radius);
    }
}