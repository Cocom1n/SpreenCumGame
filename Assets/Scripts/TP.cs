using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TP : MonoBehaviour
{
    [SerializeField] private Transform destino;
    private bool puedeEntrar;

    void Update()
    {
        if (puedeEntrar && Keyboard.current.eKey.wasPressedThisFrame)
        {
            TeleportPlayer();
        }               
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            puedeEntrar = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            puedeEntrar = false;
        }
    }

    void TeleportPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = destino.position;
        }
    }

    
}
