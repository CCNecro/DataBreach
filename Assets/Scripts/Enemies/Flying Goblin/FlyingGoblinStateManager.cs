using UnityEngine;

public class FlyingGoblinStateManager : MonoBehaviour
{
 [Header("Movement")]
    public float speed = 3f;
    public float stopDistance = 5f;
    public float retreatDistance = 3f;

    [Header("Shooting")]
    public GameObject projectilePrefab;
    public float fireRate = 2f;
    private float nextFireTime;

    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Force gravity off
        
        // Automatically try to find the player by Tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
    }

    void Update()
    {
        if (player == null) return; // Exit if no player found

        float distance = Vector2.Distance(transform.position, player.position);

        // 1. MOVEMENT LOGIC
        if (distance > stopDistance)
        {
            // Move toward player
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if (distance < retreatDistance)
        {
            // Move away from player
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }

        // 2. SHOOTING LOGIC
        if (Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }

        // 3. FLIP SPRITE
        GetComponent<SpriteRenderer>().flipX = (player.position.x < transform.position.x);
    }

    void Shoot()
    {
        if (projectilePrefab != null)
        {
            GameObject bullet = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            
            // Calculate direction to player
            Vector2 direction = (player.position - transform.position).normalized;
            
            // Give the bullet velocity
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            if(bulletRb != null) {
                bulletRb.linearVelocity = direction * 8f;
            }
        }
    }
}