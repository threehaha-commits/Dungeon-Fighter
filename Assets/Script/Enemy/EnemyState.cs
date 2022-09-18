using UnityEngine;

public class EnemyState
{
    public enum StateEnemy {Move, Patrol, Attack}
    private readonly EnemyStateEvents StateEvents;
    private readonly Transform Player;
    private readonly EnemySimpleAttackConroller AttackConroller;
    
    public EnemyState(GameObject gameObject, Transform player)
    {
        StateEvents = new EnemyStateEvents(gameObject);
        Player = player;
        AttackConroller = gameObject.GetComponentInChildren<EnemySimpleAttackConroller>();
    }
    
    public void ChangeState(StateEnemy state)
    {
        switch (state)
        {
            case StateEnemy.Move:
                Debug.Log($"State is Move");
                StateEvents.Invoke(Player.gameObject);
                AttackConroller.StopAttack();
                break;
            case StateEnemy.Patrol:
                Debug.Log($"State is Patrol");
                StateEvents.Cancel(null);
                AttackConroller.StopAttack();
                break;
            case StateEnemy.Attack:
                Debug.Log($"State is Attack");
                StateEvents.Cancel(null);
                AttackConroller.AttackTarget(Player);
                break;
        }
    }
}