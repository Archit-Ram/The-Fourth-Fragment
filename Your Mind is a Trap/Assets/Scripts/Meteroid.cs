using System.Collections;
using UnityEngine;

 public class Meteoroid : MonoBehaviour
 {
     public float damage = 0f; // Damage dealt by this meteoroid

    void Start()
    {
        StartCoroutine(Rotation());
    }

    IEnumerator Rotation()
    {
        while (true)
        {
            transform.Rotate(new Vector3(0, 0, 10f));
            yield return new WaitForSeconds(0.02f);
        }
    }

     private void OnCollisionEnter2D(Collision2D collision)
     {
         if (collision.gameObject.CompareTag("Player"))
         {
             // Apply damage to the player
             PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
             if (playerHealth != null)
             {
                 playerHealth.TakeDamage(damage);
             }

             // Destroy the meteoroid on impact
             Destroy(gameObject);
         }
     }
 }
