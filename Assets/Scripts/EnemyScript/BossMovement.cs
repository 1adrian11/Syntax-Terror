using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Destroy_score))]
public class BossMovement : MonoBehaviour
{
    public Vector2 optimize;

    // Állandó sebesség
    public Vector2 Velocity
    {
        get
        {
            Vector2 target = MovementBorder[BetweenBorders].position;
            optimize = target - (Vector2)this.transform.position;
            optimize.Normalize();
            return optimize;
        }
    }

    [field: SerializeField] public float ShootFrequency { get; private set; } // Lövés gyakorisága
    [field: SerializeField] public GameObject Bullet { get; private set; } // Lövedék prefab
    [field: SerializeField] public Transform BulletSpawn { get; private set; } // Lövedék spawn helye
    [field: SerializeField] public List<Transform> MovementBorder { get; set; } // Pontok, amely között mozog
    [field: SerializeField] public float Speed { get; set; }
    [field: SerializeField] public int BetweenBorders { get; set; } = 0;

    private int health = 100; // Életerő
    private bool hasReachedFirstPoint = false;
    private float lastShotTime;

    void Update()
    {
        Move();
        if (hasReachedFirstPoint)
        {
            Shoot();
        }
    }

    private void Move()
    {
        Vector2 targetPosition = MovementBorder[BetweenBorders].position;
        float distance = Vector2.Distance(targetPosition, transform.position);

        if (distance < 0.1f)
        {
            if (!hasReachedFirstPoint)
            {
                hasReachedFirstPoint = true; // Elérte az első pontot
                BetweenBorders = 1; // Mozogjon a második pont felé
            }
            else
            {
                BetweenBorders = (BetweenBorders == 1) ? 2 : 1; // Váltás a második és harmadik pont között
            }
        }

        Vector2 newPosition = Vector2.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
        transform.position = newPosition;
    }

    private void Shoot()
    {
        if (Time.time > lastShotTime + ShootFrequency)
        {
            GameObject bullet = Instantiate(Bullet, BulletSpawn.position, Quaternion.identity);
            lastShotTime = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            health -= 1; // Sebzés
            Destroy(other.gameObject);

            if (health <= 0)
            {
                Destroy(gameObject); // Az ellenség megsemmisül
            }
        }
    }
}