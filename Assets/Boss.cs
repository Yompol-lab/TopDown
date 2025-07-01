using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    [Header("Vida del Boss")]
    public int vida = 10;

   
    public void RecibirDaño(int daño)
    {
        vida -= daño;

        if (vida <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        SceneManager.LoadScene("Ganaste");
    }
}
