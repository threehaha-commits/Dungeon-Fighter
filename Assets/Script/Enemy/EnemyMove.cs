using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyMove : MoveHandler
{
    private Queue<Vector3> Path = new Queue<Vector3>();
    private enum MoveState { NonTarget, HasTarget }
    [SerializeField] private MoveState State = MoveState.NonTarget;
    private Vector2 OriginalPosition;
    [Inject] private PathFinder PathFinder;
    
    protected override void Start()
    {
        base.Start();
        PathFinder.transform = transform;
        OriginalPosition = MyTransform.position;
        State = MoveState.NonTarget;
        StateHandler(State);
    }

    public void SetPath(GameObject target)
    {
        Path = PathFinder.FindPath(target.transform.position);
        if (Path != null && Path.TryPeek(out Vector3 pos))
        {
            Movable.Direction = Path.Peek();
            State = MoveState.HasTarget;
        }
        else
            State = MoveState.NonTarget;

        StateHandler(State);
    }

    private void StateHandler(MoveState state)
    {
        State = state;
        switch (State)
        {
            case MoveState.NonTarget:
                Movable.Direction = GetDirectionWhenNonTarget();
                break;
            case MoveState.HasTarget:
                Movable.Direction = GetDirectionWhenHasTarget();
                break;
        }
    }

    private Vector2 GetDirectionWhenNonTarget()
    {
        float offset = 0.2f;
        float randomX = Random.Range(OriginalPosition.x - offset, OriginalPosition.x + offset);
        float randomY = Random.Range(OriginalPosition.y - offset, OriginalPosition.y + offset);
        return new Vector2(randomX, randomY);
    }

    private Vector2 GetDirectionWhenHasTarget()
    {
        if (Path.TryPeek(out Vector3 position))
        {
            float distanceForMove = (position - MyTransform.position).sqrMagnitude;
            if (distanceForMove <= 0)
                Path.Dequeue();
        }
        return position;
    }
}
