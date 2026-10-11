using System.Collections;
using UnityEngine;

public class Asustar : MonoBehaviour
{
    [Header("Screamer")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject perrito;

    [Header("Muerte")]
    [Tooltip("Arrastra aquí el Panel negro de muerte desde el Canvas.")]
    [SerializeField] private GameObject pantallamuerteObj;
    [SerializeField] private float tiempoAntesDeMorerir = 3f;
    [SerializeField] private float duracionFade = 0.5f;

    private CanvasGroup pantallamuerte;

    private bool yaAsusto = false;

    private void Start()
    {
        // Obtener o agregar CanvasGroup automáticamente
        if (pantallamuerteObj != null)
        {
            pantallamuerte = pantallamuerteObj.GetComponent<CanvasGroup>();
            if (pantallamuerte == null)
                pantallamuerte = pantallamuerteObj.AddComponent<CanvasGroup>();

            pantallamuerte.alpha = 0f;
            pantallamuerteObj.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || yaAsusto) return;

        yaAsusto = true;

        // Mostrar screamer y audio
        perrito.SetActive(true);
        audioSource.Play();

        // Congelar al jugador
        PlayerMovement movimiento = other.GetComponent<PlayerMovement>();
        if (movimiento != null) movimiento.enabled = false;

        // Iniciar secuencia de muerte
        StartCoroutine(SecuenciaMuerte(other.gameObject, movimiento));
    }

    private IEnumerator SecuenciaMuerte(GameObject player, PlayerMovement movimiento)
    {
        // Esperar unos segundos con el screamer en pantalla
        yield return new WaitForSeconds(tiempoAntesDeMorerir);

        // Fade a negro (pantalla de muerte)
        if (pantallamuerte != null)
        {
            pantallamuerteObj.SetActive(true);
            yield return StartCoroutine(Fade(0f, 1f));
        }

        // Ocultar screamer
        perrito.SetActive(false);
        audioSource.Stop();

        // Teletransportar al jugador de vuelta al portal
        if (TP.UltimoPortal != null)
        {
            player.transform.position = TP.UltimoPortal.position;
        }
        else
        {
            Debug.LogWarning("Asustar: No hay portal de retorno guardado (TP.UltimoPortal es null).");
        }

        // Pequeña pausa en negro antes de volver
        yield return new WaitForSeconds(0.5f);

        // Fade de vuelta (revivir)
        if (pantallamuerte != null)
        {
            yield return StartCoroutine(Fade(1f, 0f));
            pantallamuerteObj.SetActive(false);
        }

        // Restaurar movimiento
        if (movimiento != null) movimiento.enabled = true;

        // Permitir que el easter egg se active otra vez si vuelve
        yaAsusto = false;
    }

    private IEnumerator Fade(float desde, float hasta)
    {
        float tiempo = 0f;
        while (tiempo < duracionFade)
        {
            tiempo += Time.deltaTime;
            pantallamuerte.alpha = Mathf.Lerp(desde, hasta, tiempo / duracionFade);
            yield return null;
        }
        pantallamuerte.alpha = hasta;
    }
}

