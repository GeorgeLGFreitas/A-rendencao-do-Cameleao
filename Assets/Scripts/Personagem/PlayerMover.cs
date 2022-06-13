using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerMover : MonoBehaviour
{
    public CharacterController2D controller;
    public PlayerPos PPos;
    public Fadeout fade;
    Transform t;

    Animator anim;
    SpriteRenderer sprite;
    AudioSource p_audio;
    Rigidbody2D rb;
    public AudioClip dash_Audio;
    public CameraShaker cameraSHake;
    public float runSpeed = 40f;
    public float horizontalMove = 0f;
    public float nextDash = 0;
    public float dashCooldown = 1;
    public float dashSpeed;
    public float dashDuration = 0.5f;
    public int health = 100;
    public bool Dashb;
    public bool chave;
    static public bool invencivel;
    static public bool invDash;
    public Animator animator;
    public float afterDamageInvulnerable;
    public float indDashInvulnerable;
    public bool cheats;


    private Collider2D m_SlideCollider;
    bool jump = false;
    

    public Image coracao1;
    public Image coracao2;
    public Image coracao3;
    bool tempo;
    private float morreu;
    private float fadetime;
    private float reloadT;

    private void Start()
    {
        cheats = false;
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        invencivel = false;
        invDash = false;
        chave = false;
        tempo = false;
        rb = GetComponent<Rigidbody2D>();
        p_audio = GetComponent<AudioSource>();
        morreu = 2;
        fadetime = 0.5f;
        t = transform;
        reloadT = 0.5f;
    }
    void Update()
    {
        
        
        if(tempo)
        {
            
            animator.enabled = true;
            anim.SetTrigger("isDead");
            anim.SetBool("isOnWall", false);
            anim.SetBool("isOnGround", false);
            anim.SetBool("isClimbing", false);
            anim.SetBool("isWallSliding", false);
            anim.ResetTrigger("isJumping");
            anim.ResetTrigger("isDashing");
            anim.ResetTrigger("isWallVaulting");
            t.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            controller.m_JumpForce = 0;
            controller.m_AirControl = false;
            controller.m_Segura = false;
            morreu -= Time.deltaTime;
            fadetime -= Time.deltaTime;
            if (fadetime <= 0)
            {
                fade.FadeIn();
            }
        }
        if (morreu <=0)
        {

            reloadT -= Time.deltaTime;
            anim.SetTrigger("isDead");
            anim.SetBool("isOnWall", false);
            anim.SetBool("isOnGround", false);
            anim.SetBool("isClimbing", false);
            anim.SetBool("isWallSliding", false);
            anim.ResetTrigger("isJumping");
            anim.ResetTrigger("isDashing");
            anim.ResetTrigger("isWallVaulting");

            if (reloadT <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

        }
        if(health >= 3)
        {
            health = 3;
        }
        animator.SetFloat("isRunning", Mathf.Abs(horizontalMove));
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        if ((Input.GetButton("Dash") && Time.time > nextDash) && !controller.m_WallGrabed)
        {

            nextDash = Time.time + dashCooldown;
            Dash();

        }

        if (dashDuration < Time.time)
        {
            Dashb = false;
            animator.SetBool("isDashing", false);
        }


        if (invencivel)
        {
            afterDamageInvulnerable -= Time.deltaTime;
            if (afterDamageInvulnerable <= 0) invencivel = false;
        }

        if (invDash)
        {
            indDashInvulnerable -= Time.deltaTime;
            if (indDashInvulnerable <= 0)
            {
                invDash = false;
                indDashInvulnerable = 1;
            }
        }
        Color c = sprite.color;

        if (tempo)
        {
            c.a = 1;
        }
        else if (invDash)
        {
            c.b = 125;
            c.r = 125;
        }
        else if(invencivel)
        {
            c.a = 0.5f;
        }
        else
        {
            c.a = 1;
            c.b = 255;
            c.r = 255;
        }
        sprite.color = c;

    }
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
        if (Dashb)
        {
            controller.m_Rigidbody2D.velocity = new Vector2(controller.m_Rigidbody2D.velocity.x, 0);
        }
    }
    void Dash()
    {
        
        if (Input.GetAxis("Horizontal") > 0 && controller.m_Grounded && controller.m_LeftWall == false && controller.m_RightWall == false || Input.GetAxis("Horizontal") > 0 && controller.m_Grounded && controller.m_LeftWall == false || Input.GetAxis("Horizontal") > 0 && !controller.m_Grounded)
        {
            
            dashDuration = Time.time + 0.1f;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(dashSpeed, 0));
            Dashb = true;
            controller.m_AirControl = true;
            animator.SetBool("isDashing", true);
            animator.enabled = true;
            p_audio.PlayOneShot(dash_Audio);
            invDash = true;

        }


        if (Input.GetAxis("Horizontal") < 0 && controller.m_Grounded && controller.m_LeftWall == false && controller.m_RightWall == false || Input.GetAxis("Horizontal") < 0 && controller.m_Grounded && controller.m_LeftWall == false || Input.GetAxis("Horizontal") < 0 && !controller.m_Grounded)
        {
            
            dashDuration = Time.time + 0.1f;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-dashSpeed, 0));
            Dashb = true;
            controller.m_AirControl = true;
            animator.SetBool("isDashing", true);
            animator.enabled = true;
            p_audio.PlayOneShot(dash_Audio);
            invDash = true;
        }
        
    }
  
    public void TakeDamage(int amount)
    {
        health -= amount;
        cameraSHake.ShakeIt();
        if (health <= 0 && cheats == false)
        {
            tempo = true;
            
        }
        HealthUpdate();
        afterDamageInvulnerable = 2;
        invencivel = true;
        animator.SetBool("isDano", true);
        

    }
    void Die()
    {
      
        
            Destroy(gameObject);
       

    }
    public void HealthUpdate()
    {
        if(health == 3)
        {
            coracao1.enabled = true;
            coracao2.enabled = true;
            coracao3.enabled = true;
        }
        if(health == 2)
        {
            coracao1.enabled = true;
            coracao2.enabled = true;
            coracao3.enabled = false;
        }
        if(health == 1)
        {
            coracao1.enabled = true;
            coracao2.enabled = false;
            coracao3.enabled = false;
        }
        if (health == 0)
        {
            
            coracao1.enabled = false;
            coracao2.enabled = false;
            coracao3.enabled = false;
        }
        
    }
    public void Heal()
    {
        health++;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Chave"))
        {
            Chave c = collision.GetComponent<Chave>();
            chave = true;
        }


    }
   
    public void ParaMorte()
    {
        animator.enabled = false;
    }
  
  
}