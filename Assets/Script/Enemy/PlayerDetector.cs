using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private bool VisibleRadiusOfDistances;
    [SerializeField] private float DistanceForMoveToPlayer;
    private float DistanceForAttack = 0.2f;
    private Transform Player;
    private float RepeatingRateTime;
    private const float RateTimeWhenPlayerFar = 2f;
    private const float RateTimeWhenPlayerNear = 0.5f;
    private IEnumerator MoveHandler => CheckDistanceToPlayerForMove();
    private EnemyState EnemyStateHandler;
    
    [Inject]
    private void Construct(Player player)
    {
        Player = player.transform;
    }
    
    private void Start()
    {
        EnemyStateHandler = new EnemyState(gameObject, Player);
        GetComponent<CoroutineHandler>().AddCoroutine(this, MoveHandler);
        RepeatingRateTime = RateTimeWhenPlayerFar;
    }

    private IEnumerator CheckDistanceToPlayerForMove() 
    {
        while(gameObject.activeInHierarchy)
        {
            float distance = (Player.position - transform.position).sqrMagnitude;
            if (distance < DistanceForAttack)
            {
                EnemyStateHandler.ChangeState(EnemyState.StateEnemy.Attack);
                RepeatingRateTime = RateTimeWhenPlayerNear;
            }
            else if (distance < DistanceForMoveToPlayer)
            {
                EnemyStateHandler.ChangeState(EnemyState.StateEnemy.Move);
                RepeatingRateTime = RateTimeWhenPlayerNear;
            }
            else
            {
                EnemyStateHandler.ChangeState(EnemyState.StateEnemy.Patrol);
                RepeatingRateTime = RateTimeWhenPlayerFar;
            }
            yield return new WaitForSeconds(RepeatingRateTime);
        }
    }
    
    private void OnDrawGizmos()
    {
        if (VisibleRadiusOfDistances == false)
            return;
        Gizmos.color = new Color(0, 214, 120, 0.35F);
        Gizmos.DrawSphere(transform.position, DistanceForMoveToPlayer);
        Gizmos.color = new Color(255, 0, 0, 0.35F);
        Gizmos.DrawSphere(transform.position, DistanceForAttack);
    }
}