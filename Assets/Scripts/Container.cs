using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{

    public List<GameObject> items;
    public int capacity;
    private int itemCount;
    [SerializeField] private ItemType _itemType;
    private int _currentIndex = 0;
    public int ItemCount { get => itemCount;}
    public void AddItem()
    {
        if (itemCount < capacity)
        {
            itemCount++;
            items[itemCount - 1].SetActive(true);
            if (transform.CompareTag("Workshop"))
            {
                var spawner = GetComponent<ObjectSpawner>();
                GetComponent<BrazierEmberHandler>().Burn(spawner.cooldown);
                StartCoroutine(WaitAndSpawn(spawner.cooldown));
                spawner.DoSpawnTween();
            }
            if (transform.CompareTag("Ballista"))
            {
                //GetComponent<Ballista>().AttempFire();
            }
        }
    }

    public IEnumerator WaitAndSpawn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        GetComponent<ObjectSpawner>().SpawnItem();
        RemoveItem();
    }

    public void RemoveItem()
    {
        if (itemCount > 0)
        {
            itemCount--;
            _currentIndex--;
            items[itemCount].SetActive(false);
        }
    }
    public bool IsContainerOf(ItemType itemType)
    {
        return itemType == _itemType;
    }
    public bool IsCapacityAllowed()
    {
       return _currentIndex != items.Count;
    } 
    public Transform GetTarget()
    {
        Transform t = items[_currentIndex].transform;
        _currentIndex++;
        return t;
    } 
}
