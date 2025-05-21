using UnityEngine;

public class LazerDamageScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            FindAnyObjectByType<PlayerHealth>().TakeDamage(4f);
            Destroy(gameObject);
        }
    }
}
