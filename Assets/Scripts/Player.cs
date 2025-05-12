using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // reiniciar a cena

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

    //morte por queda
    private float alturaMax;
    public float alturaMorte = 5f;
    private bool isDead = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        alturaMax = transform.position.y; //Altura máxima
    }

    void Update()
    {
        if (isDead) return; //Não faz nada se já estiver morto

        Move();
        Jump();
        Tiro();

        //Maior altura já alcançada
        if (transform.position.y > alturaMax)
        {
            alturaMax = transform.position.y;
        }

        //Verifica a altura da queda
        if (transform.position.y < alturaMax - alturaMorte)
        {
            Die();
        }

        //Animações de pulo e queda
        if (!isGrounded)
        {
            if (rb2d.velocity.y > 0f)
            {
                animator.SetBool("isJumping", true);
                animator.SetBool("isFalling", false);
            }
            else if (rb2d.velocity.y < -0f)
            {
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", true);
            }
        }
        else
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Jogador morreu por queda.");
        // Reinicia a cena atual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Tiro()
    {
        if (Input.GetKeyDown(shootCode) && Time.time >= currentTime)
        {
            Instantiate(power, new Vector2(transform.position.x, transform.position.y - bottomValue), Quaternion.identity);
            currentTime = Time.time + cooldown;
        }
    }

    void Move()
    {
        float horizontalMove = (playerIndex == 0) ? Input.GetAxis("HorizontalP1") : Input.GetAxis("HorizontalP2");

        animator.SetBool("isRunning", horizontalMove != 0);

        if (horizontalMove > 0)
            GetComponent<SpriteRenderer>().flipX = false;
        else if (horizontalMove < 0)
            GetComponent<SpriteRenderer>().flipX = true;

        rb2d.velocity = new Vector2(horizontalMove * speed * Time.deltaTime, rb2d.velocity.y);
    }

    void Jump()
    {
        bool jumpKey = (playerIndex == 0 && Input.GetKeyDown(KeyCode.M)) ||
                       (playerIndex == 1 && Input.GetKeyDown(KeyCode.Keypad2));

        if (jumpKey && isGrounded)
        {
            rb2d.AddForce(new Vector2(rb2d.velocity.x, jump), ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
            animator.SetBool("isFalling", false);
        }
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
