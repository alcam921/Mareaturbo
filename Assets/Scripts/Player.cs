using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 2.5f;
    public float jump = 1f;
    public float bottomValue = 0f;
    private bool isGrounded = false;
    private Rigidbody2D rb2d; 
    public GameObject power;
    public float currentTime;
    public float cooldown;
    public int playerIndex = 0;
    public KeyCode shootCode;
    public CameraFollow cam;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        Move();
        Jump();
        Tiro();
        
    }
    void Tiro()
    {
        
        if (Input.GetKeyDown(shootCode) && Time.time >= currentTime)
        {
            Instantiate(power, new Vector2(gameObject.transform.position.x,gameObject.transform.position.y-bottomValue),Quaternion.identity);
            currentTime = Time.time + cooldown;
        }
    }
    void Move()
    {
        
        float horizontalMove = 2;
        if (playerIndex == 0)
        {
             horizontalMove = Input.GetAxis("HorizontalP1");

        }
        else
        {
             horizontalMove = Input.GetAxis("HorizontalP2");
        }
        if(horizontalMove != 0)
        {
        animator.SetBool("isRunning", true);

        }
        else
        {
            animator.SetBool("isRunning", true);
        }
        Debug.Log(horizontalMove);
        rb2d.velocity = new Vector2(horizontalMove * speed * Time.deltaTime, rb2d.velocity.y);

    }
    void Jump()
    {
        if (playerIndex == 0)
        {
            //horizontalMove = Input.GetAxis("Horizontal");

            if (Input.GetKeyDown(KeyCode.M) && isGrounded)
            {
                rb2d.AddForce(new Vector2(rb2d.velocity.x, jump),ForceMode2D.Impulse);

            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Keypad2) && isGrounded)
            {

                rb2d.AddForce(new Vector2(rb2d.velocity.x, jump), ForceMode2D.Impulse);
            }
        }
    }
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            isGrounded = true;  
        }

    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            isGrounded = false;
        }
    }

}

