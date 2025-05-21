using UnityEngine;

public class MovingLazerScript : MonoBehaviour
{
    float Speed;
    Vector2 NewPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NewPos = new Vector2(this.transform.position.x + Random.Range(-50, 50),6);
        Speed = Random.Range(1, 5);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, NewPos , Speed * Time.deltaTime);
    }
}
