using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
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

    public bool IsGrounded;
    public bool check;
    public LayerMask groundMask;
    // Start is called before the first frame update
    void Start()
    {
        PlayerAnimations = GetComponent<Animator>();
        IsGrounded = false;
        PlayerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Rb = gameObject.GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        

        InputX = Input.GetAxis("Horizontal");

        check = Physics2D.Raycast(GroundCheckPos.position, Vector2.down, 0.02f, groundMask);
        IsGrounded = check;

        /*
        
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
        */


        // if ((IsGrounded) && (!JumpCoolDown) && (Input.GetKey("w")))
        // {
        //     Rb.linearVelocityY += JumpForce * 10f * Time.deltaTime;
        // }

        if (InputX < 0)
        {
            PlayerSpriteRenderer.flipX = true;
        }
        else if (InputX > 0)
        {
            PlayerSpriteRenderer.flipX = false;
        }

        if ((IsGrounded) && (Input.GetKeyDown("w")))
        {
            FindAnyObjectByType<AudioController>().PlayAudioOnce(0);
            Vector3 Pos = gameObject.transform.position + new Vector3(0, -1f, 0);
            Instantiate(JumpParticles, Pos, Quaternion.identity);
            Rb.linearVelocityY += JumpForce;
        }

        PlayAnimations();

        
    }

    private void FixedUpdate()
    {
        Rb.linearVelocity = Vector3.right * InputX * Speed + new Vector3(0, Rb.linearVelocity.y);
                
    }

    private void LateUpdate()
    {
        // if ((LastJumpStatus != IsGrounded) && (IsGrounded))
        // {
        //     LastJumpStatus = IsGrounded;
        //     Vector3 Pos = gameObject.transform.position + new Vector3(0, -1f, 0);
        //     Instantiate(LandParticles, Pos, Quaternion.identity);
        // }
        // else if (LastJumpStatus != IsGrounded)
        // {
        //     LastJumpStatus = IsGrounded;
        // }
    }

    void PlayAnimations()
    {
        if ((IsGrounded) && (Mathf.Abs(InputX) > 0))
        {
            FindAnyObjectByType<AudioController>().PlayAudioLoop(1);
            PlayerAnimations.SetBool("IsRunning", true);
        }
        else if((IsGrounded) && (InputX==0))
        {
            FindAnyObjectByType<AudioController>().StopPlaying();
            PlayerAnimations.SetBool("IsRunning", false);
        }
        
        if (!IsGrounded)
        {
            PlayerAnimations.SetBool("Jump",true);
            PlayerAnimations.SetBool("IsRunning", false);
        }
        else
        {
            PlayerAnimations.SetBool("Jump",false);
        }
    }

}
