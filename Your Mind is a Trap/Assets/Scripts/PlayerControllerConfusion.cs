using System.Collections;
using UnityEngine;

public class PlayerControllerConfusion : MonoBehaviour
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
    bool check;

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


        if (Mathf.Abs(Rb.linearVelocity.y) > 5)
        {
            IsGrounded = false;
        }


        InputX = GameInputScript.GetAxis("Forward", "Backward");

        check = Physics2D.Raycast(GroundCheckPos.position, Vector2.down, 0.1f);

        if ((check == false) && (IsGrounded == true))
        {
            StartCoroutine(JumpBuffer(0.5f));
        }
        else if ((check != IsGrounded) && (check == true))
        {
            IsGrounded = check;
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





        if ((!JumpCoolDown) && (GameInputScript.GetKey("Jump")))
        {
            Rb.linearVelocity += Vector2.up * JumpForce * 10f * Time.deltaTime;
        }

        if (InputX < 0)
        {
            PlayerSpriteRenderer.flipX = true;
        }
        else
        {
            PlayerSpriteRenderer.flipX = false;
        }

        if ((IsGrounded) && (GameInputScript.GetKeyDown("Jump")) && (JumpCoolDown))
        {
            FindAnyObjectByType<AudioController>().PlayAudioOnce(0);
            Vector3 Pos = gameObject.transform.position + new Vector3(0, -1f, 0);
            Instantiate(JumpParticles, Pos, Quaternion.identity);
            Rb.linearVelocity += Vector2.up * JumpForce;
            JumpCoolDown = false;
            StartCoroutine(CayoteTime(0.15f));
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

    IEnumerator JumpBuffer(float BufferTime)
    {
        Debug.Log("HII");
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
        if ((IsGrounded) && (Rb.linearVelocity.magnitude > 0))
        {
            FindAnyObjectByType<AudioController>().PlayAudioLoop(1);
            PlayerAnimations.SetBool("IsRunning", true);
        }
        else if ((IsGrounded) && (Rb.linearVelocity.magnitude == 0))
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

