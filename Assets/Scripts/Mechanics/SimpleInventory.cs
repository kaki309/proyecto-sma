using System.Collections.Generic;
using UnityEngine;

public class SimpleInventory : MonoBehaviour
{
    private List<string> items = new List<string>();

    public void AddItem(string itemID)
    {
        if (!items.Contains(itemID))
        {
            items.Add(itemID);
            Debug.Log("Item añadido: " + itemID);
        }
    }

    public void RemoveItem(string itemID)
    {
        if (items.Contains(itemID))
        {
            items.Remove(itemID);
            Debug.Log("Item removido: " + itemID);
        }
    }

    public bool HasItem(string itemID)
    {
        return items.Contains(itemID);
    }

    public List<string> GetItems()
    {
        return items;
    }
}