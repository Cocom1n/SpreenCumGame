using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    [Header("Dialogos")]
    [SerializeField] private TMP_Text npcNameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Image npcPortrait;

    [Header("Botoncitos")]
    [SerializeField] private Button optionAButton;
    [SerializeField] private TMP_Text optionAText;
    [SerializeField] private Button optionBButton;
    [SerializeField] private TMP_Text optionBText;

    [Header("ItemFrame")]
    [SerializeField] private GameObject itemPanel;
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private Image itemIconImage;

    [SerializeField] private PlayerInventory inventory;

    private NpcDialogue currentNpc;

    void Awake()
    {
        if (inventory == null) inventory = FindFirstObjectByType<PlayerInventory>();

        if (optionAButton != null) optionAButton.onClick.AddListener(() => OnOptionClicked(0));
        if (optionBButton != null) optionBButton.onClick.AddListener(() => OnOptionClicked(1));

        Hide();
    }

    public void Show(NpcDialogue npc, string npcName, string text, Sprite portrait)
    {
        currentNpc = npc;

        if (panel != null) panel.SetActive(true);
        if (npcNameText != null) npcNameText.text = npcName;
        if (dialogueText != null) dialogueText.text = text;

        if (npcPortrait != null)
        {
            npcPortrait.sprite = portrait;
            npcPortrait.gameObject.SetActive(portrait != null);
        }
    }

    public void ShowChoices(string optionA, string optionB)
    {
        if (optionAText != null) optionAText.text = optionA;
        if (optionBText != null) optionBText.text = optionB;
        if (optionAButton != null) optionAButton.gameObject.SetActive(true);
        if (optionBButton != null) optionBButton.gameObject.SetActive(true);
    }

    public void HideChoices()
    {
        if (optionAButton != null) optionAButton.gameObject.SetActive(false);
        if (optionBButton != null) optionBButton.gameObject.SetActive(false);
    }

    public void ShowItem(string itemName, Sprite icon)
    {
        if (itemPanel == null) return;

        itemPanel.SetActive(true);
        if (itemNameText != null) itemNameText.text = itemName;
        if (itemIconImage != null)
        {
            itemIconImage.sprite = icon;
            itemIconImage.gameObject.SetActive(icon != null);
        }
    }

    public void Hide()
    {
        currentNpc = null;
        HideChoices();
        if (itemPanel != null) itemPanel.SetActive(false);
        if (panel != null) panel.SetActive(false);
    }

    private void OnOptionClicked(int index)
    {
        if (currentNpc != null) currentNpc.Choose(index, inventory);
    }
}
