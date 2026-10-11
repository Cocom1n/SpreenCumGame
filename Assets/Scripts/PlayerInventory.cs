using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private GameObject[] slotImages;

    private readonly List<string> items = new List<string>();

    private readonly Dictionary<string, int> itemSlotMap = new Dictionary<string, int>
    {
        { "Leche",     0 },
        { "Chocolate", 1 },
        { "Harina",    2 },
        { "Huevo",     3 }
    };

    public IReadOnlyList<string> Items => items;

    void Awake()
    {
        if (slotImages != null)
        {
            for (int i = 0; i < slotImages.Length; i++)
            {
                if (slotImages[i] != null)
                    slotImages[i].SetActive(false);
            }
        }
    }

    public void AddItem(string itemName)
    {
        items.Add(itemName);
        Debug.Log($"Obtuviste: {itemName}");

        if (slotImages != null && itemSlotMap.TryGetValue(itemName, out int index))
        {
            if (index < slotImages.Length && slotImages[index] != null)
                slotImages[index].SetActive(true);
        }
    }

    public bool HasItem(string itemName) => items.Contains(itemName);

    public void ClearItems()
    {
        items.Clear();
        if (slotImages != null)
        {
            for (int i = 0; i < slotImages.Length; i++)
            {
                if (slotImages[i] != null)
                    slotImages[i].SetActive(false);
            }
        }
    }
}
