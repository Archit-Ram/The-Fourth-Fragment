using UnityEngine;

public class GiantBallBehavior : MonoBehaviour
{
    public Transform player; // Reference to the player
    public GameObject batsPrefab; // Prefab for bats
    public int numberOfBats = 8; // Number of bats to spawn
    public float playerHealthReduction = 20f; // Amount to reduce player health

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Access the PlayerController script to reduce health
            PlayerHealth playerController = collision.GetComponent<PlayerHealth>();
            if (playerController != null)
            {
                playerController.TakeDamage(playerHealthReduction);
            }
            Debug.Log("Collided");
            // Spawn bats around the ball
            SpawnBatsAroundBall();

            // Destroy the ball after engulfing the player
            Destroy(gameObject);
        }
    }

    void SpawnBatsAroundBall()
    {
        float radius = 1.5f; // Radius for bats to spawn around the ball
        for (int i = 0; i < numberOfBats; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfBats; // Divide full circle into equal parts
            Vector2 spawnPosition = new Vector2(
                transform.position.x + Mathf.Cos(angle) * radius,
                transform.position.y + Mathf.Sin(angle) * radius
            );

            GameObject bat = Instantiate(batsPrefab, spawnPosition, Quaternion.identity);

            // Make bats move toward the player
            Rigidbody2D rb = bat.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (player.position - (Vector3)spawnPosition).normalized;
                rb.linearVelocity = direction * 3f; // Adjust speed as needed
            }

            Destroy(bat, 5f); // Destroy the bat after a duration
        }
    }
}
