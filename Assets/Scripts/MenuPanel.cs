using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject panelMenu;

    [Tooltip("Panel de créditos")]
    public GameObject panelCreditos;

    [Header("Botones")]
    public Button botonIniciar;

    [Tooltip("Botón para mostrar/ocultar los créditos")]
    public Button botonCreditos;

    private bool juegoIniciado = false;

    private void Start()
    {
        panelMenu.SetActive(true);
        panelCreditos.SetActive(false);
        Time.timeScale = 0f;

        botonIniciar.onClick.AddListener(IniciarJuego);
        botonCreditos.onClick.AddListener(ToggleCreditos);
    }

    private void Update()
    {
        if (juegoIniciado && Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            ToggleMenu();
        }
    }

    public void IniciarJuego()
    {
        juegoIniciado = true;
        panelMenu.SetActive(false);
        panelCreditos.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ToggleCreditos()
    {
        panelCreditos.SetActive(!panelCreditos.activeSelf);
    }

    public void ToggleMenu()
    {
        bool menuActivo = panelMenu.activeSelf;

        if (menuActivo)
        {
            // Cerrar menú → reanudar juego
            panelMenu.SetActive(false);
            panelCreditos.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            // Abrir menú → pausar juego
            panelMenu.SetActive(true);
            panelCreditos.SetActive(false);
            Time.timeScale = 0f;
        }
    }
}

