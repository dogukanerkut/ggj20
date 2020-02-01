using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{

    public List<GameObject> items;
    private int itemCount;
    [SerializeField] private ItemType _itemType;
    private int _currentIndex = 0;
    public void AddItem()
    {
        if (itemCount < 3)
        {
            itemCount++;
            items[itemCount - 1].SetActive(true);
        }
    }

    public void RemoveItem()
    {
        if (itemCount > 0)
        {
            itemCount--;
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
