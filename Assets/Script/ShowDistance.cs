using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDistance : MonoBehaviour
{
    public bool Show;
    public Transform Enemy;
    public Transform Player;

    private void Update()
    {
        if (Show == false)
            return;
        float distance1 = (Player.position - transform.position).magnitude;
        float distance2 = (Enemy.position - transform.position).magnitude;
        Debug.Log(distance1 / distance2);
    }
}
