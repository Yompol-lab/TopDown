using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform gunLeft;
    public Transform gunRight;
    public Transform gunExtra1;
    public Transform gunExtra2;
    public float fireRate = 0.1f;
    public float bulletSpeed = 50f;
    public HUDManager hudManager;

    private bool isShooting = false;
    private float shootTimer;

    private bool extraGunsActive = false;

    public AudioSource gunAudio;

    void Update()
    {
        if (isShooting)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0f)
            {
                Shoot();
                shootTimer = fireRate;
            }
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isShooting = true;
            shootTimer = 0f;

            if (gunAudio != null && !gunAudio.isPlaying)
                gunAudio.Play();
        }
        else if (context.canceled)
        {
            isShooting = false;

            if (gunAudio != null && gunAudio.isPlaying)
                gunAudio.Stop();
        }
    }

    void Shoot()
    {
        FireBulletFrom(gunLeft);
        FireBulletFrom(gunRight);

        if (extraGunsActive)
        {
            FireBulletFrom(gunExtra1);
            FireBulletFrom(gunExtra2);
        }
    }

    void FireBulletFrom(Transform gun)
    {
        GameObject bullet = Instantiate(bulletPrefab, gun.position, gun.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = gun.forward * bulletSpeed;
        }
    }

    public void ActivateExtraGuns(float duration)
    {
        if (!extraGunsActive)
        {
            extraGunsActive = true;
            hudManager?.ActivatePowerUpTimer(duration);
            Invoke(nameof(DeactivateExtraGuns), duration);
        }
    }


    void DeactivateExtraGuns()
    {
        extraGunsActive = false;
    }
}
