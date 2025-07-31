using UnityEngine;

public class bulletPrefab : MonoBehaviour
{
    public float lifeTime = 5f;
    public float speed = 20f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

       
        rb.linearVelocity = transform.forward * speed;

        
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        
        Destroy(gameObject);
    }
}