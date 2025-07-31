using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int vidaMaxima = 100;
    private int vidaActual;

    public Image barraVida;
    public GameObject panelPerdiste;


    public AudioClip sonidoDaño;
    private AudioSource audioSource;

    void Start()
    {
        vidaActual = vidaMaxima;
        ActualizarBarra();

        if (panelPerdiste != null)
            panelPerdiste.SetActive(false);


        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void RecibirDaño(int cantidad)
    {
        vidaActual -= cantidad;


        if (sonidoDaño != null && audioSource != null)
            audioSource.PlayOneShot(sonidoDaño);

        if (vidaActual <= 0)
        {
            vidaActual = 0;
            Morir();
        }

        ActualizarBarra();
    }

    public void RecibirVida(int cantidad)
    {
        vidaActual += cantidad;
        if (vidaActual > vidaMaxima)
            vidaActual = vidaMaxima;

        ActualizarBarra();
    }

    void ActualizarBarra()
    {
        if (barraVida != null)
        {
            barraVida.fillAmount = (float)vidaActual / vidaMaxima;
        }
    }

    void Morir()
    {
        SceneManager.LoadScene("Muerte2");


    }
}