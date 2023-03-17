using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float Speed = 0.03f;


    [SerializeField]
    float Jump = 10f;

    int maxHealth = 5;

    public int score;
    public Transform holder;
    TextMeshProUGUI healthText;
    TextMeshProUGUI scoreText;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    CircleCollider2D Ccollider2D;
    BoxCollider2D Bcollider2D;
    Transform myTransform;
    SpriteRenderer mySprite;
    Rigidbody2D rb;
    Animator anim;
     int PlayerHealth = 3; 
      private bool isWallSliding;
    private float wallSlidingSpeed = 2f;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f); 
    private bool doubleJump;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    bool isJump;

    [SerializeField] private TrailRenderer tr;
    [SerializeField] private bool isGrounde;
    [SerializeField] private bool IsWalle;
    // Start is called before the first frame update
    void Start()
    {
        //isJump = false;
        //float myFloat = 5.5f;
        PlayerHealth = maxHealth;
        score = 0;
        myTransform = GetComponent<Transform>();
        mySprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
         Ccollider2D = GetComponent<CircleCollider2D>();
        Bcollider2D = GetComponent<BoxCollider2D>();
        healthText = holder.Find("TxtHealth").GetComponent<TextMeshProUGUI>();
        scoreText = holder.Find("TxtScore").GetComponent<TextMeshProUGUI>();
        scoreText.text = "Score :" + score; //"Score : 0";
        healthText.text = PlayerHealth + "/" + maxHealth; //"5/5";
        

    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }
        if(myTransform.position.y <= -45f||PlayerHealth < 1)
        {
            GameOver.dead = true;
        }
        else
        {
             GameOver.dead = false;
            
        }
        isGrounde = isGrounded();
        IsWalle = IsWalled();
        WallSlide();
        WallJump();
        respawn();
        //if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        //{
        //   transform.Translate(new Vector3(-1 * Speed, 0));
        //    mySprite.flipX = true;
        //}
        //else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        //{ 
        //  transform.Translate(new Vector3(Speed, 0));

        //    mySprite.flipX = false;

        //    if (Input.GetButtonDown("Jump"))
        //        rb.velocity = new Vector2(0, Jump);
        //}
        //Debug.Log(Input.GetAxis("Horizotal"));
        //rb.velocity = new Vector2(Speed, rb.velocity.y);
        //rb.velocity = new Vector2(Speed * Input.GetAxis("Horizontal") * Time.deltaTime, rb.velocity.y);

        if (Input.GetAxis("Horizontal") > 0)
            mySprite.flipX = false;
        else if (Input.GetAxis("Horizontal") < 0)
            mySprite.flipX = true;

        if (isGrounded() && !Input.GetButton("Jump"))
        {
            doubleJump = false;
        }
         if (Input.GetButtonDown("Dash") && canDash)
        {
            StartCoroutine(Dash());
        }

        if (Input.GetButtonDown("Jump") )
        {
            if (isGrounded() || doubleJump)
            {
            rb.velocity = new Vector2(rb.velocity.x, Jump);
            doubleJump = !doubleJump;
            }
        }
        if (Mathf.Abs(rb.velocity.y) < 0.01f){
            
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        }

        
       
        
    }
    private void FixedUpdate()
    {
         if (isDashing)
        {
            return;
        }
        rb.velocity = new Vector2(Speed * Input.GetAxis("Horizontal"), rb.velocity.y);
        //Debug.Log(Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(0, Jump);
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enamy"))
        {

         if (isGrounded() == false && rb.velocity.y < 0)
            {
                Destroy(collision.gameObject);

            }
            else
            {
                PlayerHealth = PlayerHealth - 1;
                //Debug.Log("Current Health: " + PlayerHealth + "/" + maxHealth);
                healthText.text = PlayerHealth + "/" + maxHealth;
                if (PlayerHealth <= 1)
                    GameOver.dead = true;
                    Debug.Log("You are Died");
            }
        }
        else if(collision.CompareTag("Gem"))
        {
            score += 100;
            scoreText.text = "Score: " + score;
            Destroy(collision.gameObject);

        }
        else if (collision.CompareTag("Cherry"))
        {
            score += 50;
            scoreText.text = "Score: " + score;
            Destroy(collision.gameObject);

        }
        else if (collision.CompareTag("Star"))
        {
            if (PlayerHealth < maxHealth)
                PlayerHealth++;

            //Debug.Log("Current Health: " + PlayerHealth + "/" + maxHealth);
            healthText.text = PlayerHealth + "/" + maxHealth;
            Destroy(collision.gameObject);

        }
        else if(collision.CompareTag("Finish")){
            LevelEnd.finish = true;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enamy"))
        {
            if (!isGrounded() && rb.velocity.y < 0)
            {
                Destroy(collision.gameObject);

            }
            else
            {
                if(PlayerHealth <= 1){
                    PlayerHealth = 0;
                    GameOver.dead = true;

                }
                else {PlayerHealth = PlayerHealth - 1;}
                //Debug.Log("Current Health: " + PlayerHealth + "/" + maxHealth);
                healthText.text = PlayerHealth + "/" + maxHealth;
            }
        }
    }
 private void WallSlide()
    {
        if (IsWalled() &&!isGrounded() && Input.GetAxisRaw("Horizontal") != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }
private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                mySprite.flipX = !mySprite.flipX;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }
    private void StopWallJumping()
    {
        isWallJumping = false;
    }
    private bool isGrounded()
    {
       
        RaycastHit2D raycastHit = Physics2D.Raycast(Ccollider2D.bounds.center,Vector2.down,Ccollider2D.bounds.extents.y+ .5f,groundLayer);
        Color raycolor;
        if(raycastHit.collider != null) {
            raycolor = Color.green;
        }
        else{
            raycolor = Color.red;
        }

        Debug.DrawRay(Ccollider2D.bounds.center, Vector2.down * (Ccollider2D.bounds.extents.y + 1.5f), Color.red);
        Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }
     private bool IsWalled()
    {
       RaycastHit2D raycastHit = Physics2D.Raycast(Ccollider2D.bounds.center,Vector2.right,Ccollider2D.bounds.extents.y+ 1.5f,wallLayer);
       RaycastHit2D raycastHit2 = Physics2D.Raycast(Ccollider2D.bounds.center,Vector2.left,Ccollider2D.bounds.extents.y+ 1.5f,wallLayer);
        Color raycolor;
        if(raycastHit.collider != null) {
            raycolor = Color.green;
        }
        else{
            raycolor = Color.red;
        }

        Debug.DrawRay(Ccollider2D.bounds.center, Vector2.right * (Ccollider2D.bounds.extents.y + 1.5f), Color.red);
        Debug.Log(raycastHit.collider);
        return ((raycastHit.collider != null && !mySprite.flipX) || (raycastHit2.collider != null && mySprite.flipX));
    }
    public void isDead(){
        if(PlayerHealth <= 0)
        {}
    }
    public void respawn(){
        if(Input.GetButtonDown("Respawn")){
SceneManager.LoadScene("Level 1");}
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        if(mySprite.flipX == true){
        rb.velocity = new Vector2(transform.localScale.x * dashingPower*-1, 0f);}
        else{rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);}
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);

        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
