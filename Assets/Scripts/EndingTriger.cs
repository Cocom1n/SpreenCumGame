using UnityEngine;
using UnityEngine.InputSystem;

// Ponlo en un objeto con Collider2D. Funciona tanto con "Is Trigger" activado como desactivado.
public class EndingTriger : MonoBehaviour
{
    [SerializeField] private EndingManager endingManager;
    [SerializeField] private Key interactKey = Key.E;

    private bool playerInside;
    private bool endingShown;

    void Awake()
    {
        if (endingManager == null) endingManager = FindFirstObjectByType<EndingManager>();
    }

    void Update()
    {
        if (!playerInside || endingShown) return;

        Keyboard kb = Keyboard.current;
        if (kb != null && kb[interactKey].wasPressedThisFrame)
        {
            endingShown = true;
            if (endingManager != null) endingManager.ShowEnding();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) { if (IsPlayer(other)) playerInside = true; }
    private void OnTriggerExit2D(Collider2D other) { if (IsPlayer(other)) playerInside = false; }

    private void OnCollisionEnter2D(Collision2D collision) { if (IsPlayer(collision.collider)) playerInside = true; }
    private void OnCollisionExit2D(Collision2D collision) { if (IsPlayer(collision.collider)) playerInside = false; }

    private bool IsPlayer(Collider2D other)
    {
        return other.GetComponentInParent<PlayerInventory>() != null;
    }
}
