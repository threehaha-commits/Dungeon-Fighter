using UnityEngine;
using UnityEngine.UIElements;

public class Chest : MonoBehaviour
{
    private Item[] Items;
    private VisualElement Root;
    private DropWindowVisible Window;
    
    public void SetItems(Item[] items)
    {
        Items = items;
    }

    private void Start()
    {
        Window = GetComponent<DropWindowVisible>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            DropWindow.Create(Items);
            Window.Open();
            gameObject.SetActive(false);
        }
    }
}
