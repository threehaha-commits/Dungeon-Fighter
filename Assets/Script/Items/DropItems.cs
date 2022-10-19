using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Zenject;

public class DropItems : MonoBehaviour, IDeathInspector
{
    [SerializeField] private Item[] Items;
    [SerializeField] private int[] Chance;
    [Inject] private IDropBoxFactory Factory;

    private void Start()
    {
        Chance = new int[Items.Length];
        for(int i = 0;i < Items.Length; i++)
            Chance[i] = Items[i].DropChance();

        Test();
    }

    private void Test()
    {
        List<Item> dropList = new List<Item>();

        dropList.Add(Items[0]);
        dropList.Add(Items[1]);
        dropList.Add(Items[2]);
        dropList.Add(Items[3]);
        dropList.Add(Items[4]);
        SendDropListToFactory(dropList);
        gameObject.SetActive(false);
    }
    
    void IDeathInspector.ApplyDamage(Health health)
    {
        if (health.IsDeath)
        {
            List<Item> dropList = new List<Item>();
            AddItemInDropList(dropList);
            SendDropListToFactory(dropList);
        }
    }

    private void AddItemInDropList(List<Item> dropList)
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (ItemWillDrop(i))
                dropList.Add(Items[i]);
        }
    }

    private void SendDropListToFactory(List<Item> dropList)
    {
        if (dropList.Count > 0)
        {
            //Debug.Log("Drop count is " + dropList.Count);
            Factory.Create(dropList.ToArray(), DropPosition(transform.position));
        }
    }

    private bool ItemWillDrop(int id) 
    {
        int random = UnityEngine.Random.Range(0, Chance[id] + 1);
        if (random == Chance[id])
            return true;
        else
            return false;
    }

    private Vector2 DropPosition(Vector2 position)
    {
        float offset = 0.2f;
        float randomX = UnityEngine.Random.Range(position.x - offset, position.x + offset);
        float randomY = UnityEngine.Random.Range(position.y - offset, position.y + offset);
        return new Vector2(randomX, randomY);
    }
}
