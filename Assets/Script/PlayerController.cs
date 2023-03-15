using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    bool isJump;
    // Start is called before the first frame update
    void Start()
    {
        isJump = false;
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


        if (Input.GetButtonDown("Jump") && isGrounded() )
        {
            rb.velocity = new Vector2(0, Jump);
            
        }
        if (Mathf.Abs(rb.velocity.y) < 0.01f){
            
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        }

        
       
        
    }
    private void FixedUpdate()
    {
        
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
                if (PlayerHealth <= 0)
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

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enamy"))
        {
            if (isJump && rb.velocity.y < 0)
            {
                Destroy(collision.gameObject);

            }
            else
            {
                PlayerHealth = PlayerHealth - 1;
                //Debug.Log("Current Health: " + PlayerHealth + "/" + maxHealth);
                healthText.text = PlayerHealth + "/" + maxHealth;
            }
        }
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

}
