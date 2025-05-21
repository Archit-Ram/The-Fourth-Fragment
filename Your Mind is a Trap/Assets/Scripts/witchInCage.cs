using UnityEngine;

public class witchInCage : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed=0;
    private Rigidbody2D body;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x + speed * Time.fixedDeltaTime, body.linearVelocity.y);
    }
    void OnCollisionEnter2D(Collision2D coll){
        transform.localScale*=-1;
    }
}
