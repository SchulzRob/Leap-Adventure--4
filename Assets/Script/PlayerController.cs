using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float Sp = 0.03f;


    [SerializeField]
    float Jump = 10f;

    Transform myTransform;
    SpriteRenderer mySprite;
    Rigidbody2D rb;
    Animator anim;
    public int PlayerHealth = 3;  

    bool isJump;
    // Start is called before the first frame update
    void Start()
    {
        isJump = false;
        float myFloat = 5.5f;
        myTransform = GetComponent<Transform>();
        mySprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();  

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
        if (Input.GetButtonDown("Jump") && !isJump)
        {
            rb.velocity = new Vector2(0, Jump);
            isJump = true;
        }
        if (Mathf.Abs(rb.velocity.y) < 0.01f)
            isJump = false;
            anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(Sp * Input.GetAxis("Horizontal"), rb.velocity.y);
        //Debug.Log(Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enamy"))
            PlayerHealth--;
        Debug.Log(PlayerHealth);
        if (PlayerHealth <= 0)
            Debug.Log("You are Died");
    }

}
