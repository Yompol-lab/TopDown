using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    public int puntosEnemigos = 0;
    public int puntosItems = 0;
    public int puntosParaGanar = 10;

    public TextMeshProUGUI textoPuntosEnemigos;
    public TextMeshProUGUI textoPuntosItems;

   

    public AudioSource audioSource;

    private void Awake()
    {
        if (instancia == null) instancia = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        Time.timeScale = 1f;

        ActualizarTextoPuntosEnemigos();
        ActualizarTextoPuntosItems();
    }

    public void SumarPuntoEnemigo()
    {
        puntosEnemigos++;
        ActualizarTextoPuntosEnemigos();
       
    }

    public void SumarPuntoItem()
    {
        puntosItems++;
        ActualizarTextoPuntosItems();
    }

    void ActualizarTextoPuntosEnemigos()
    {
        if (textoPuntosEnemigos != null)
            textoPuntosEnemigos.text = puntosEnemigos.ToString();
    }

    void ActualizarTextoPuntosItems()
    {
        if (textoPuntosItems != null)
            textoPuntosItems.text = puntosItems.ToString();
    }

   

    void CambiarEscena()
    {
        SceneManager.LoadScene("Nivel2");
    }

    
    public void EliminarPersonaje(GameObject personaje)
    {
        if (personaje.CompareTag("Boss"))
        {
            CambiarEscena();
        }
        else
        {
            SumarPuntoEnemigo();
        }

        Destroy(personaje);
    }
}
