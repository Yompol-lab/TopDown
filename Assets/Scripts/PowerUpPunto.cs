using UnityEngine;

public class PowerUpPunto : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instancia.SumarPuntoItem();
            Destroy(gameObject);
        }
    }
}