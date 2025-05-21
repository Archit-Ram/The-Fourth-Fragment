using UnityEngine;

public class NerfBall : MonoBehaviour
{
    public float speed; // Speed of the nerf ball.
    public float debuffDuration = 3f; // Duration for which the player's speed will be halved.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Apply the debuff to the player.
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                StartCoroutine(ApplyDebuff(player));
            }

            // Destroy the nerf ball after hitting the player.
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // Destroy the nerf ball if it hits the ground.
            Destroy(gameObject);
        }
    }

    private System.Collections.IEnumerator ApplyDebuff(PlayerController player)
    {
        // Halve the player's movement and jump speed.
        player.Speed /= 2;
        player.JumpForce /= 2;

        // Wait for the debuff duration.
        yield return new WaitForSeconds(debuffDuration);

        // Restore the player's original speed and jump force.
        player.Speed *= 2;
        player.JumpForce *= 2;
    }
}
