using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingProjectile : MonoBehaviour
{
    public Transform target; // The target to home in on

    private Rigidbody2D rb; // Rigidbody for movement

    public float speed = 5f; // Speed of the projectile

    public float rotationSpeed = 200f; // Speed of rotation towards the target

    private int obstacleLayer;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform; // Find the player by tag
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        obstacleLayer = LayerMask.NameToLayer("Obstacle"); // Cache the Obstacle layer index
    }

    void FixedUpdate()
    {
        Vector2 direction = (Vector2)target.position - rb.position; // Calculate the direction to the target

        direction.Normalize(); // Normalize the direction vector

        float rotateAmount = Vector3.Cross(direction, transform.up).z; // Calculate the rotation amount

        rb.angularVelocity = -rotateAmount * rotationSpeed; // Apply rotation to the projectile

        rb.linearVelocity = transform.up * speed; // Set the velocity of the projectile
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerResources playerRes = other.GetComponent<PlayerResources>();
            if (playerRes != null)
            {
                playerRes.PlayerTakeDamage(2); // Deal damage to the player
            }
            Destroy(gameObject); // Destroy the projectile after hitting the player
        }
        else if (other.gameObject.layer == obstacleLayer)
        {
            Destroy(gameObject); // Destroy the projectile after hitting an obstacle
        }
    }
}