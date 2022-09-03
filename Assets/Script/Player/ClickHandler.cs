using UnityEngine;
using Zenject;

public class ClickHandler : MonoBehaviour
{
    private IMovable Movable;
    private Collider2D EnemyCollider;
    [SerializeField, Inject] private TargetHandler Target;
    
    [Inject]
    private void Construct(Document Document)
    {
        Movable = GetComponent<PlayerMove>();
        MouseOverUI.AddElement(Document.Skill, "AbilityMain");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (MouseOverUI.On)
                return;

            Collider2D hitCollider = GetCollider();
            if (hitCollider != null)
            {
                if (IsEnemy(hitCollider) == false)
                    return;

                Target.SetTarget(hitCollider.transform);
                if(IsDoubleClick(hitCollider))
                    Movable.Direction = GetPoint();
                EnemyCollider = hitCollider;
                return;
            }
            Movable.Direction = GetPoint();
        }
    }

    private bool IsEnemy(Collider2D collider)
    {
        return collider.tag.Equals("Enemy");
    }

    private bool IsDoubleClick(Collider2D collider)
    {
        return EnemyCollider == collider;
    }

    private Collider2D GetCollider()
    {
        Vector2 point = GetPoint();
        int getLayerMask = 1 << 6;
        int dropBoxLayerMask = ~getLayerMask;
        RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero, dropBoxLayerMask);
        return hit.collider;
    }

    private Vector2 GetPoint() 
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
