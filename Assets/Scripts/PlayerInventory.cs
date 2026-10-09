using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private readonly List<string> items = new List<string>();

    public IReadOnlyList<string> Items => items;

    public void AddItem(string itemName)
    {
        items.Add(itemName);
        Debug.Log($"Obtuviste: {itemName}");
    }

    public bool HasItem(string itemName) => items.Contains(itemName);
}
