using UnityEngine;

public class BalaEnemigo : MonoBehaviour
{
    public int dañoAlJugador = 10;

    void Start()
    {
        Destroy(gameObject, 10f); 
    }

    void OnCollisionEnter(Collision other)
    {
        VidaJugador jugador = other.collider.GetComponent<VidaJugador>();
        if (jugador != null)
        {
            jugador.RecibirDaño(dañoAlJugador);
        }

        Destroy(gameObject);
    }
}