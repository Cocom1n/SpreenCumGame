using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class Ending
{
    public string endingName;

    [Header("poner los nombres exactos perro")]
    public string[] requiredItems;
    public string title;
    public Sprite image;
}

public class EndingManager : MonoBehaviour
{
    [SerializeField] private Ending[] endings;

    [Header("Final por defecto")]
    [SerializeField] private Ending defaultEnding;

    [Header("ImagenesFinales")]
    [SerializeField] private GameObject endingPanel;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private Image endingImage;

    [SerializeField] private PlayerInventory inventory;

    void Awake()
    {
        if (inventory == null) inventory = FindFirstObjectByType<PlayerInventory>();
        if (endingPanel != null) endingPanel.SetActive(false);
    }

    public void ShowEnding()
    {
        Ending ending = FindEnding();
        if (ending == null)
        {
            Debug.LogWarning("EndingManager: ningún final coincide y no hay final por defecto configurado.");
            return;
        }

        if (endingPanel != null) endingPanel.SetActive(true);
        if (titleText != null) titleText.text = ending.title;
        if (endingImage != null)
        {
            endingImage.sprite = ending.image;
            endingImage.gameObject.SetActive(ending.image != null);
        }

        Debug.Log($"Final: {ending.endingName}");
    }

    public void HideEnding()
    {
        if (endingPanel != null) endingPanel.SetActive(false);
    }

    private Ending FindEnding()
    {
        HashSet<string> owned = ToSet(inventory != null ? inventory.Items : null);

        if (endings != null)
        {
            foreach (Ending ending in endings)
            {
                if (owned.SetEquals(ToSet(ending.requiredItems)))
                    return ending;
            }
        }

        return defaultEnding;
    }

    private static HashSet<string> ToSet(IEnumerable<string> items)
    {
        HashSet<string> set = new HashSet<string>();
        if (items == null) return set;

        foreach (string item in items)
        {
            if (!string.IsNullOrWhiteSpace(item))
                set.Add(item.Trim().ToLowerInvariant());
        }
        return set;
    }
}
