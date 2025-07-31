using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float duration = 15f;

    void OnTriggerEnter(Collider other)
    {
        PlayerShooting player = other.GetComponent<PlayerShooting>();
        if (player != null)
        {
            player.ActivateExtraGuns(duration);
            Destroy(gameObject);
        }
    }
}
