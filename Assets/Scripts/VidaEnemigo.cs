using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VidaEnemigo : MonoBehaviour
{
    [Header("Vida")]
    public float vidaMaxima = 100f;
    private float vidaActual;

    [Header("Daño recibido")]
    public float dañoRecibidoPorBala = 20f;

    [Header("UI")]
    public GameObject barraVidaPrefab;
    public Transform puntoBarraVida;

    private Image barraVida;
    private GameObject instanciaBarra;

    [Header("Sonido")]
    public AudioClip sonidoMuerte;
    private AudioSource audioSource;

    void Start()
    {
        vidaActual = vidaMaxima;

        if (barraVidaPrefab != null && puntoBarraVida != null)
        {
            instanciaBarra = Instantiate(barraVidaPrefab, puntoBarraVida.position, Quaternion.identity, puntoBarraVida);
            barraVida = instanciaBarra.transform.Find("HealthFill")?.GetComponent<Image>();

            if (barraVida == null)
                Debug.LogWarning("No se encontró el componente Image llamado 'HealthFill'");
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        ActualizarBarra();
    }

    public void RecibirDaño(float cantidad)
    {
        vidaActual -= cantidad;
        if (vidaActual <= 0)
        {
            vidaActual = 0;
            Morir();
        }
        ActualizarBarra();
    }

    void ActualizarBarra()
    {
        if (barraVida != null)
        {
            barraVida.fillAmount = vidaActual / vidaMaxima;
        }
    }

    void Morir()
    {
        if (CompareTag("Boss"))
        {
            // Si es el Boss, cambio de escena directamente
            SceneManager.LoadScene("Ganaste");
            return;
        }

        if (GameManager.instancia != null)
            GameManager.instancia.SumarPuntoEnemigo();

        // Reproducir sonido desde un objeto temporal
        if (sonidoMuerte != null)
        {
            GameObject tempAudio = new GameObject("SonidoMuerte");
            AudioSource tempSource = tempAudio.AddComponent<AudioSource>();
            tempSource.clip = sonidoMuerte;
            tempSource.Play();
            Destroy(tempAudio, sonidoMuerte.length);
        }

        Destroy(instanciaBarra);
        Destroy(gameObject);
    }
}
