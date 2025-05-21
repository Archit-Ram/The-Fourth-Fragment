using System.Collections;
using UnityEngine;

public class PlayerMovement4 : MonoBehaviour
{
    public Transform GroundCheckPos1;
    public Transform GroundCheckPos2;
    public Transform GroundCheckPos3;

    public float Speed;
    public float JumpForce;
    public GameObject JumpParticles;
    public GameObject LandParticles;
    
    Rigidbody Rb;
    Animator PlayerAnimations;
    SpriteRenderer PlayerSpriteRenderer;

    //public GameObject MovementParticles;

    float InputX;

    bool JumpCoolDown;
    bool LastJumpStatus;
    public bool IsGrounded;
    public bool check;
    private Vector3 next_level = new Vector3(311, 160, 0);
    public LayerMask groundMask;
    public UnityEngine.SceneManagement.Scene next_scene;
    // Start is called before the first frame update
    void Start()
    {
        PlayerAnimations = GetComponent<Animator>();
        IsGrounded = false;
        JumpCoolDown = true;
        PlayerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Rb = gameObject.GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if(Mathf.Abs(Rb.linearVelocity.y) > 5)
        {
            IsGrounded = false ;
        }
        

        InputX = Input.GetAxis("Horizontal");
        if((Physics.Raycast(GroundCheckPos1.position, Vector3.down, 0.2f))||(Physics.Raycast(GroundCheckPos2.position, Vector3.down, 0.2f))|| Physics.Raycast(GroundCheckPos3.position, Vector3.down, 0.2f))
        {
            check = true ;
        }
        else
        {
            check = false ;
        }

        if ((check == false) && (IsGrounded == true))
        {
            StartCoroutine(JumpBuffer(0.5f));        
        }
        else if ((check != IsGrounded) && (check == true))
        {
            IsGrounded=check;
        }

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

        if ((transform.position - next_level).magnitude < 10f)
        {
            print("transition");
            FindAnyObjectByType<SceneLoader>().LoadNextLevel();
        }


        if ((!JumpCoolDown) && (Input.GetKey("w")))
        {
            Rb.linearVelocity += Vector3.up * JumpForce * 10f * Time.deltaTime;
        }

        if (InputX < 0) 
        {
            PlayerSpriteRenderer.flipX = true;
        }
        else
        {
            PlayerSpriteRenderer.flipX = false;
        }

        if ((IsGrounded) && (Input.GetKeyDown("w")) && (JumpCoolDown))
        {
            Vector3 Pos = gameObject.transform.position + new Vector3(0, -1.2f, 0);
            Instantiate(JumpParticles, Pos, Quaternion.identity);
            Rb.linearVelocity += Vector3.up * JumpForce;
            JumpCoolDown = false;
            StartCoroutine(CayoteTime(0.15f));
        }

        PlayAnimations();

        
    }

    private void FixedUpdate()
    {
        Rb.linearVelocity = Vector3.right * InputX * Speed + new Vector3(0, Rb.linearVelocity.y);

        Rb.AddForce(4*Physics.gravity*Rb.mass);
        
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

    IEnumerator JumpBuffer(float BufferTime)
    {
        yield return new WaitForSeconds(BufferTime);
        IsGrounded = check;
    }
    IEnumerator CayoteTime(float DelayTime)
    {
        yield return new WaitForSeconds(DelayTime);
        JumpCoolDown = true;

    }




    void PlayAnimations()
    {
        if ((IsGrounded) && (Rb.linearVelocity.magnitude > 0.1f)) 
        {
            PlayerAnimations.SetBool("IsRunning", true);
        }
        else if((IsGrounded) && (Rb.linearVelocity.magnitude <= 0.1f))
        {
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

