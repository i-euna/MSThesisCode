using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireInterval = 2f;
    public float bulletSpeed = 5f;
    private float fireTimer;

    void Start()
    {
        fireTimer = fireInterval;
    }

    void Update()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            Fire();
            fireTimer = fireInterval;
        }
    }

    void Fire()
    {
        //Vector3 newPos = new Vector3(1.7f, -2.04f, 0f);
        GameObject bullet = Instantiate(bulletPrefab,
            firePoint);
        bullet.transform.parent = null;
        bullet.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f); ;
        //.SetParent(null);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.left * bulletSpeed;
        }
    }
}
