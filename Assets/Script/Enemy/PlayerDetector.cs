using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private float DistanceForAgressive;
    [SerializeField] private Transform Player;
    private float RepeatingRate;
    private float RateWhenEnemyFar = 2f;
    private float RateWhenEnemyNear = 0.5f;
    private EnemyAbilitiesEvents AbilitiesEvents;
    private IEnumerator PathFinder => CheckDistanceToPlayer();

    [Inject]
    private void Construct(Player Player)
    {
        this.Player = Player.transform;
    }

    private void Start()
    {
        GetComponent<CoroutineHandler>().AddCoroutine(this, PathFinder);
        AbilitiesEvents = new EnemyAbilitiesEvents(gameObject);
        RepeatingRate = RateWhenEnemyFar;
    }

    private IEnumerator CheckDistanceToPlayer() 
    {
        while(gameObject.activeInHierarchy)
        {
            float disntace = (Player.position - transform.position).sqrMagnitude;
            if (disntace < DistanceForAgressive)
            {
                AbilitiesEvents.InvokeAction(Player.gameObject);
                RepeatingRate = RateWhenEnemyNear;
            }
            else
            {
                AbilitiesEvents.Cancel(null);
                RepeatingRate = RateWhenEnemyFar;
            }
            yield return new WaitForSeconds(RepeatingRate);
        }
    }
}
