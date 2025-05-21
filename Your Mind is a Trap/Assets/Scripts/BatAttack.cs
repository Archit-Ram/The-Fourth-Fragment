using UnityEngine;

public class BatAttack : MonoBehaviour
{
    public float speed = 5;
    Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }
    void FixedUpdate()
    {

        if (transform.position.y<-3.5f){
            Destroy(gameObject);
        }
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        Destroy(gameObject, 3);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(5f);
        }
        Destroy(gameObject); 
    }
}