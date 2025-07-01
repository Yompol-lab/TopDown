using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 30f;


    public AudioClip sonidoDisparo;
    private AudioSource audioSource;

    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * bulletSpeed;
        }


        if (sonidoDisparo != null)
        {
            audioSource.PlayOneShot(sonidoDisparo);
        }
    }
}