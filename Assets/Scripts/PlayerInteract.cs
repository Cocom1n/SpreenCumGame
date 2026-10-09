using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInventory))]
public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float interactRadius = 1.5f;
    [SerializeField] private Key interactKey = Key.E;

    private NpcDialogue nearbyNpc;
    private PlayerInventory inventory;

    void Awake()
    {
        inventory = GetComponent<PlayerInventory>();
    }

    void Update()
    {
        NpcDialogue previous = nearbyNpc;
        nearbyNpc = FindClosestNpc();
        if (previous != null && previous != nearbyNpc)
        {
            previous.SetSpriteActive(false);
        }
        if (nearbyNpc != null)
        {
            nearbyNpc.SetSpriteActive(true);
        }

        if (previous != null && previous != nearbyNpc && previous.IsTalking)
        {
            previous.EndDialogue();
        }

        if (nearbyNpc == null) return;

        Keyboard kb = Keyboard.current;
        if (kb == null) return;

        if (nearbyNpc.IsWaitingChoice)
        {
            if (kb.digit1Key.wasPressedThisFrame) nearbyNpc.Choose(0, inventory);
            else if (kb.digit2Key.wasPressedThisFrame) nearbyNpc.Choose(1, inventory);
        }
        else if (kb[interactKey].wasPressedThisFrame)
        {
            nearbyNpc.Interact();
        }
    }

    private NpcDialogue FindClosestNpc()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactRadius);

        NpcDialogue closest = null;
        float closestDist = float.MaxValue;

        foreach (Collider2D hit in hits)
        {
            NpcDialogue npc = hit.GetComponentInParent<NpcDialogue>();
            if (npc == null) continue;

            float dist = ((Vector2)hit.transform.position - (Vector2)transform.position).sqrMagnitude;
            if (dist < closestDist)
            {
                closestDist = dist;
                closest = npc;
            }
        }

        return closest;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
