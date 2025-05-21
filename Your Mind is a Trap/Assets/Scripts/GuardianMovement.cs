using System.Collections;
using UnityEngine;

public class GuardianMovement : MonoBehaviour
{
    private bool m_FacingRight = true;
    private Transform player;

	public float attackRange;
    public float speed;
    public float lineOfSight;

    Animator GuardianAnimator;
    private void Start()
    {
        GuardianAnimator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void FixedUpdate()
    {

        float distanceFromPLayer = Vector2.Distance(player.position, transform.position);
        if(distanceFromPLayer < lineOfSight && distanceFromPLayer>attackRange )
        {
            GuardianAnimator.SetBool("IsWalking", true);
            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x,this.transform.position.y), speed * Time.deltaTime);
            Move(player.transform.position.x - this.transform.position.x, false);
        }
        else
        {
            GuardianAnimator.SetBool("IsWalking", false);

        }
        float xDirr = transform.position.x - player.position.x;
        Move(xDirr, false);
        
        
    }



    public void Move(float move, bool jump)
    {
            Debug.Log(move);
            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;
        Vector3 flipped = transform.localScale;
        flipped.x *= -1f;
        transform.localScale = flipped;

        transform.Rotate(0f, 180f, 0f);
    }

}

