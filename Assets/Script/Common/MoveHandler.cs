using UnityEngine;
using UnityEngine.EventSystems;

public interface IMovable
{
    Vector2 Direction { get; set; }
    void Move(Vector2 position);
}

public class MoveHandler : MonoBehaviour, IMovable
{
    protected IMovable Movable;
    Vector2 IMovable.Direction { get; set; }
    protected Rigidbody2D Rigidbody2D;
    [SerializeField] protected float Speed;
    protected Transform MyTransform;

    protected virtual void Start()
    {
        Movable = this;
        MyTransform = transform;
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void IMovable.Move(Vector2 position)
    {
        if (position == Vector2.zero)
            return;
        
        Rigidbody2D.MovePosition(Vector3.MoveTowards(MyTransform.position, Movable.Direction, Speed * Time.fixedDeltaTime));
    }
}
