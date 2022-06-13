using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    public PlayerMover PlayerMov;
    [SerializeField] public float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] public bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] public LayerMask m_WhatIsGround;                           // A mask determining what is ground to the character
    [SerializeField] public Transform m_GroundCheck;                            // A position marking where to check if the player is grounded.
    [SerializeField] public Transform m_LeftWallcheck;
    [SerializeField] public Transform m_RightWallcheck;
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

    public const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    public bool m_Grounded;            // Whether or not the player is grounded.
    private bool m_DoubleJump;
    public bool m_LeftWall;
    public bool m_RightWall;
    public float WallJumpCooldown;
    public float NextWallJump;
    public bool m_WallGrabed;
    public bool m_WallSlide;
    public bool m_Segura;
    Animator anim;
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    public Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;



    public Collider2D[] colliders;
    public Collider2D[] collidersLeftWall;
    public Collider2D[] collidersRightWall;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;
    public UnityEvent OnWallGrabEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool m_wasCrouching = false;
    public AudioClip Passo1;
    public AudioClip Passo2;
    public AudioClip pulo;
    public AudioClip aterissa;
    public AudioClip wallgrab;
    public AudioClip Chave;
    public AudioClip Coracao;
    public Animation puloAni;
    AudioSource p_Audio;
    AudioSource p_Audio2;
    AudioSource p_Audio3;

    private void Start()
    {
        anim = GetComponent<Animator>();
        p_Audio = GetComponents<AudioSource>()[0];
        p_Audio2 = GetComponents<AudioSource>()[1];
        p_Audio3 = GetComponents<AudioSource>()[2];
    }
    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }


    public void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;
        m_LeftWall = false;
        m_RightWall =false;
      



        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject && PlayerMov.health >0)
            {
                m_Grounded = true;
                m_DoubleJump = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }

        collidersLeftWall = Physics2D.OverlapCircleAll(m_LeftWallcheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < collidersLeftWall.Length; i++)
        {
            if (collidersLeftWall[i].gameObject != gameObject)
            {

                m_LeftWall = true;

            }
        }
        collidersRightWall = Physics2D.OverlapCircleAll(m_RightWallcheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < collidersRightWall.Length; i++)
        {
            if (collidersRightWall[i].gameObject != gameObject)
            {

                m_RightWall = true;

            }
        }
    }



    public void Move(float move, bool crouch, bool jump)
    {
        // If crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {

            // If crouching
            if (crouch)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Reduce the speed by the crouchSpeed multiplier
                move *= m_CrouchSpeed;

                // Disable one of the colliders when crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = false;
            }
            else
            {
                // Enable the collider when not crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = true;

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

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
        // If the player should do A DoubleJump..
        if (m_DoubleJump && jump && !m_Grounded)
        {
            
            p_Audio.PlayOneShot(pulo, 4.5f);
            anim.ResetTrigger("isJumping");
            anim.SetTrigger("isJumping");
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0f);
            m_DoubleJump = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce * 0.85f));
             

        }
        if(m_DoubleJump && jump && !m_Grounded && PlayerMov.health > 0)
        {
            anim.enabled = true;
        }

        //for sound
        
        //for animations
        /*if(jump)
        {
            anim.SetTrigger("isJumping");
        }*/
        if (m_Grounded)
        {
            
           
            anim.SetBool("isClimbing", false);
            anim.SetBool("isOnWall", false);
            anim.SetBool("isWallSliding", false);
            anim.ResetTrigger("isJumping");
            anim.SetBool("isDano", false);


        }
        if(m_Grounded && PlayerMov.health > 0)
        {
            anim.SetBool("isOnGround", true);
            anim.enabled = true;
            anim.SetBool("isDano", false);
        }
        if(!m_Grounded && !m_WallGrabed && !m_LeftWall)
        {
           
            anim.SetTrigger("isJumping");

        }

        if(!m_Grounded && m_LeftWall && !m_RightWall && m_WallGrabed)
        {
           
            anim.SetTrigger("isWallVaulting");

        }
        else if (!m_Grounded && !m_LeftWall && !m_RightWall && !m_WallGrabed)
        {
           
            anim.ResetTrigger("isWallVaulting");
        }

        if(m_Grounded && m_LeftWall == true && m_RightWall == true || m_Grounded && m_LeftWall == true)
        {
          
            anim.SetBool("isTouchingWall", true);
            
        }

        else
        {
            anim.SetBool("isTouchingWall", false);
        }

        if (Input.GetButton("Segura") && PlayerMov.health > 0)
        {
            m_Segura = true;
        }
        else
        {
            m_Segura = false;

        }
        
        // If the player should Grab the Wall..
        if (m_RightWall && m_LeftWall &&!m_Grounded && m_Segura)
        {

            anim.enabled = true;
            OnWallGrabEvent.Invoke();
            anim.SetBool("isOnWall", true);
            anim.SetBool("isOnGround", false);
            anim.SetBool("isWallSliding", false);
            anim.SetBool("isClimbing", false);
            anim.ResetTrigger("isJumping");
            PlayerMov.Dashb = false;
            m_WallGrabed = true;
            m_WallSlide = false;
            m_DoubleJump = false;
            m_AirControl = false;
            m_Rigidbody2D.gravityScale = 0;
            m_Rigidbody2D.mass = 1;
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0f);
            

            //Slide Down the wall
            if (Input.GetButton("Vertical") && Input.GetAxisRaw("Vertical") < 0)
            {
                m_WallSlide = true;
                m_Rigidbody2D.gravityScale = 300;
                m_Rigidbody2D.velocity = new Vector2(0f, 0f);
                anim.SetBool("isOnWall", false);
                anim.SetBool("isOnGround", false);
                anim.SetBool("isClimbing", false);
                anim.SetBool("isWallSliding", true);
                anim.SetBool("isDano", false);
                anim.ResetTrigger("isJumping");
                anim.ResetTrigger("isWallVaulting");
                jump = false;
                if(!p_Audio2.isPlaying)
                {
                    p_Audio2.Play();
                }
            }
            

            //Climb the wall
            if (Input.GetButton("Vertical") && Input.GetAxisRaw("Vertical") > 0)
            {
                m_Rigidbody2D.gravityScale = -120;
                jump = false;
                m_WallSlide = false;
                m_Rigidbody2D.velocity = new Vector2(0f, 0f);
                anim.SetBool("isOnWall", false);
                anim.SetBool("isOnGround", false);
                anim.SetBool("isClimbing", true);
                anim.SetBool("isWallSliding", false);
                anim.SetBool("isDano", false);

            }
            

            // If the player should do a Wall Jump..
            if (m_FacingRight && Input.GetAxis("Horizontal") < 0 && jump)
            {
                p_Audio.PlayOneShot(pulo, 4.5f);
                m_Rigidbody2D.AddForce(new Vector2(-m_JumpForce, m_JumpForce));
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0f);
                Flip();
                m_AirControl = true;
            }
            else if (!m_FacingRight && Input.GetAxis("Horizontal") > 0 && jump)
            {
                p_Audio.PlayOneShot(pulo, 4.5f);
                m_Rigidbody2D.AddForce(new Vector2(m_JumpForce, m_JumpForce));
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0f);
                Flip();
                m_AirControl = true;
            }
           

        }
   
        else
        {
            anim.SetBool("isDano", false);
            m_WallGrabed = false;
            m_WallSlide = false;
            anim.SetBool("isOnWall", false);
            //anim.SetBool("isOnGround", true);
            anim.SetBool("isClimbing", false);
            m_Rigidbody2D.gravityScale = 30;
            m_Rigidbody2D.mass = 1f;
            

        }

        //Audio Wall Slide
        if (!p_Audio2.isPlaying && m_WallSlide)
        {
            p_Audio2.Play();
        }
        else if (p_Audio2.isPlaying && !m_WallSlide)
        {
            p_Audio2.Stop();
        }
        
       

        // If the player should Vault
        if (m_LeftWall && !m_RightWall && Input.GetButton("Vertical"))
        {
            m_WallGrabed = false;
            m_AirControl = true;
            m_JumpForce = 1000;
           
            if (m_RightWall)
            {
                m_Rigidbody2D.AddForce(new Vector2(move * 10, 600));
            }
            if (!m_RightWall)
            {
                m_Rigidbody2D.AddForce(new Vector2(move * 10, 600));
            }
        }
        else
        {
            m_JumpForce = 4000;
        }
        

        // If the player should jump...
        if (m_Grounded && jump)
        {
            p_Audio.PlayOneShot(pulo, 4.5f);
            anim.SetTrigger("isJumping");
            anim.SetBool("isDano", false);
            m_Grounded = false;
            m_AirControl = true;
            m_DoubleJump = true;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));

        }

        if (!m_Grounded)
        {
            m_MovementSmoothing = 0.07f;
            anim.SetBool("isOnGround", false);
        }

        else
            m_MovementSmoothing = 0.05f;

       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Chave"))
        {
            p_Audio.PlayOneShot(Chave);
        }
        if (collision.CompareTag("Vida"))
        {
            p_Audio.PlayOneShot(Coracao, 1.5f);
        }

    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    public void passo1()
    {
        p_Audio.PlayOneShot(Passo1, 0.7f);
    }
    public void passo2()
    {
        p_Audio.PlayOneShot(Passo2, 0.7f);
    }
    public void wallGrab()
    {
        p_Audio.PlayOneShot(wallgrab);
    }
    public void land()
    {
        p_Audio.PlayOneShot(aterissa, 4.5f);
    }
    public void Parapulo()
    {
        anim.enabled = false;

    }
    
}



