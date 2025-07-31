using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float duration = 15f;
    public float speed = 7.0f;


    void Update()
    {
        transform.position += Vector3.back * speed * Time.deltaTime;
    }


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
