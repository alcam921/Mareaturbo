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
        // Verifica o estado do personagem para animações de pulo e queda
        if (!isGrounded)
        {
            if (rb2d.velocity.y > 0f)
            {
                // Está subindo (pulo)
                animator.SetBool("isJumping", true);
                animator.SetBool("isFalling", false);
            }
            else if (rb2d.velocity.y < -0f)
            {
                // Está caindo
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", true);
            }
        }
        else
        {
            // Está no chão
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }


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
            animator.SetBool("isRunning", false);
        }
        if (horizontalMove > 0) 
        {
            
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (horizontalMove < 0)
        {
            
            GetComponent<SpriteRenderer>().flipX = true;
        }
        
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
                // Ativar a animação de pulo
                animator.SetBool("isJumping", true);
                animator.SetBool("isFalling", false);

            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Keypad2) && isGrounded)
            {

                rb2d.AddForce(new Vector2(rb2d.velocity.x, jump), ForceMode2D.Impulse);
                // Ativar a animação de pulo
                animator.SetBool("isJumping", true);
                animator.SetBool("isFalling", false);
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

