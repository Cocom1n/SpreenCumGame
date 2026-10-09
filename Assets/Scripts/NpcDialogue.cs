using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class DialogueOption
{
    public string text;//texto de la opcion                       
    [TextArea(2, 4)] public string npcReply; //lo q responde el npc segun la respuesta :p 
    public bool givesItem;//activar soolo si es la opcion q da el item                
}

[Serializable]
public class DialogueLine
{
    [TextArea(2, 4)] public string text;

    [Header("Opciones (opcional)")]
    public bool hasChoice;
    public DialogueOption optionA;
    public DialogueOption optionB;
}

public class NpcDialogue : MonoBehaviour
{
    [SerializeField] private string npcName;
    [SerializeField] private Sprite portrait;
    [SerializeField] private string itemName;
    [SerializeField] private GameObject interactSprite;
    [SerializeField] private Sprite itemIcon;
    [SerializeField] private DialogueLine[] lines;
    [SerializeField] private DialogueUI ui;

    private int currentLine = -1;
    private bool waitingChoice;
    private bool itemAlreadyGiven;

    public bool IsTalking => currentLine >= 0;
    public bool IsWaitingChoice => waitingChoice;

    void Awake()
    {
        if (ui == null) ui = FindFirstObjectByType<DialogueUI>();
        if (interactSprite != null)
        {
            interactSprite.SetActive(false);
        }
    }

    public void Interact()
    {
        if (lines == null || lines.Length == 0) return;
        if (waitingChoice) return; // hay que elegir una opción primero

        currentLine++;

        if (currentLine >= lines.Length)
        {
            EndDialogue();
            return;
        }

        DialogueLine line = lines[currentLine];

        if (ui != null)
        {
            ui.Show(this, npcName, line.text, portrait);
            ui.HideChoices();
        }

        if (line.hasChoice)
        {
            waitingChoice = true;
            if (ui != null) ui.ShowChoices(line.optionA.text, line.optionB.text);
        }
    }

    // index: 0 = opción A, 1 = opción B
    public void Choose(int index, PlayerInventory inventory)
    {
        if (!waitingChoice) return;

        DialogueLine line = lines[currentLine];
        DialogueOption option = index == 0 ? line.optionA : line.optionB;

        waitingChoice = false;

        if (ui != null)
        {
            ui.HideChoices();
            ui.Show(this, npcName, option.npcReply, portrait);
        }

        if (option.givesItem && !itemAlreadyGiven)
        {
            itemAlreadyGiven = true;
            if (inventory != null) inventory.AddItem(itemName);
            if (ui != null) ui.ShowItem(itemName, itemIcon);
        }
    }

    public void EndDialogue()
    {
        SetSpriteActive(false);
        currentLine = -1;
        waitingChoice = false;
        if (ui != null) ui.Hide();
    }

    public void SetSpriteActive(bool active)
    {
        if (interactSprite != null)
        {
            interactSprite.SetActive(active);
        }
    }
}

