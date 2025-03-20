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
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector2(speed * Time.deltaTime, 0));
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
        }
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)&& isGrounded)
        {
            rb2d.AddForce(new Vector2(rb2d.velocity.x, jump),ForceMode2D.Impulse);
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

