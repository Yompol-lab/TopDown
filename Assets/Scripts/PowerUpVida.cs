using UnityEngine;

public class PowerUpVida : MonoBehaviour
{
    public int cantidadVida = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            VidaJugador vidaJugador = other.GetComponent<VidaJugador>();
            if (vidaJugador != null)
            {
                vidaJugador.RecibirVida(cantidadVida);
            }

            Destroy(gameObject);
        }
    }
}