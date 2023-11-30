using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject gun;
    public GameObject bulletPrefab;
    public AudioClip bulletEffect;
    public float shootingIntervalMin = 1f;
    public float shootingIntervalMax = 3f;
    public float bulletSpeed = 100f;

    private float nextShootTime;

    void Start()
    {
        ScheduleNextShot();
    }

    void Update()
    {
        TryShoot();
    }

    void TryShoot()
    {
        if (Time.time >= nextShootTime)
        {
            Shoot();
            ScheduleNextShot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, gun.transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = gun.transform.forward * bulletSpeed;

        if (bulletEffect != null)
            AudioSource.PlayClipAtPoint(bulletEffect, gun.transform.position);

        // Destroy the bullet after a certain time to save memory
        Destroy(bullet, 5f);
    }

    void ScheduleNextShot()
    {
        nextShootTime = Time.time + Random.Range(shootingIntervalMin, shootingIntervalMax);
    }
}
