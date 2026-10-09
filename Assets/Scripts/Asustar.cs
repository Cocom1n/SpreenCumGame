using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asustar : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject perrito;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            perrito.SetActive(true);
            audioSource.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            perrito.SetActive(false);
            audioSource.Stop();
        }
    }
}
