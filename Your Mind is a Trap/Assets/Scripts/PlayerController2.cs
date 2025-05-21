using System.Collections;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public Transform GroundCheckPos;

    public float Speed;
    public float JumpForce;
    public GameObject JumpParticles;
    public GameObject LandParticles;
    
    Rigidbody2D Rb;
    Animator PlayerAnimations;
    SpriteRenderer PlayerSpriteRenderer;

    //public GameObject MovementParticles;

    float InputX;

    bool JumpCoolDown;
    bool LastJumpStatus;
    bool IsGrounded;
    bool IsSwinging;
    public LayerMask enemyMask;
    public LayerMask groundMask;

    // Start is called before the first frame update
    void Start()
    {
        PlayerAnimations = GetComponent<Animator>();
        IsGrounded = false;
        JumpCoolDown = true;
        PlayerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Rb = gameObject.GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {   
        
        if(Mathf.Abs(Rb.linearVelocity.y) > 0)
        {
            IsGrounded = false ;
        }
        InputX = Input.GetAxis("Horizontal");

        IsGrounded = Physics2D.Raycast(GroundCheckPos.position, Vector2.down,0.1f, groundMask);

        //For calculating distance from ground
        
        if(Physics2D.Raycast(GroundCheckPos.position, Vector2.down, 0.2f))
        {
            //start particles
            RaycastHit2D Hit = Physics2D.Raycast(GroundCheckPos.position, Vector2.down);
        }
        else
        {
            //stop particles
        }
        

        
        

        if ((!JumpCoolDown) && (Input.GetKey("w")))
        {
            Rb.linearVelocity += Vector2.up * JumpForce * 8f * Time.deltaTime;
        }

        if (InputX < 0)
        {
            PlayerSpriteRenderer.flipX = true;
        }
        else if (InputX > 0)
        {
            PlayerSpriteRenderer.flipX = false;
        }

        if ((IsGrounded) && (Input.GetKeyDown("w")) && (JumpCoolDown))
        {
            FindAnyObjectByType<AudioController>().PlayAudioOnce(0);
            Vector3 Pos = gameObject.transform.position + new Vector3(0, -1f, 0);
            Instantiate(JumpParticles, Pos, Quaternion.identity);
            Rb.linearVelocity += Vector2.up * JumpForce;
            JumpCoolDown = false;
            StartCoroutine(CayoteTime(0.1f));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindAnyObjectByType<AudioController>().PlayAudioOnce(2);
            print("Swinging");
            StartCoroutine(Swing());
        }

        PlayAnimations();


        
    }

    private void FixedUpdate()
    {
        Rb.linearVelocity = Vector2.right * InputX * Speed + new Vector2(0, Rb.linearVelocity.y);

        
    }

    private void LateUpdate()
    {
        if ((LastJumpStatus != IsGrounded) && (IsGrounded))
        {
            LastJumpStatus = IsGrounded;
            Vector3 Pos = gameObject.transform.position + new Vector3(0, -1f, 0);
            Instantiate(LandParticles, Pos, Quaternion.identity);
        }
        else if (LastJumpStatus != IsGrounded)
        {
            LastJumpStatus = IsGrounded;
        }
    }

    IEnumerator CayoteTime(float DelayTime)
    {
        yield return new WaitForSeconds(DelayTime);
        JumpCoolDown = true;
    }
    IEnumerator Swing()
    {
        if (!IsSwinging) {
            IsSwinging = true;
            foreach (Collider2D enemy in Physics2D.OverlapBoxAll(transform.position + Vector3.right*(PlayerSpriteRenderer.flipX ? -1 : 1), new Vector2(2, 2), 0, enemyMask))
            {
                print("witch");
                enemy.GetComponent<Health>().TakeDamage(3);
            }
            yield return new WaitForSeconds(0.4f);
            IsSwinging = false;
        }
    }


    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        //Gizmos.DrawCube(transform.position + Vector3.right*(PlayerSpriteRenderer.flipX ? -1 : 1), new Vector2(2, 2));
    }
    #endif

    void PlayAnimations()
    {
        if (IsSwinging)
        {
            PlayerAnimations.SetTrigger("IsSwinging");
        }
        else
        {
            if ((IsGrounded) && (Mathf.Abs(InputX) > 0))
            {
                FindAnyObjectByType<AudioController>().PlayAudioLoop(1);
                PlayerAnimations.SetBool("IsRunning", true);
            }
            else if ((IsGrounded) && (InputX == 0))
            {
                FindAnyObjectByType<AudioController>().StopPlaying();
                PlayerAnimations.SetBool("IsRunning", false);
            }

            if (!IsGrounded)
            {
                PlayerAnimations.SetBool("Jump", true);
                PlayerAnimations.SetBool("IsRunning", false);
            }
            else
            {
                PlayerAnimations.SetBool("Jump", false);
            }
        }
        

        
        
    }

}

