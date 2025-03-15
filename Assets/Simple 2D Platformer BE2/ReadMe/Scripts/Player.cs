using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 2.5f;
    public float jump = 1f;
    private bool isGrounded = false;
    private Rigidbody2D rb2d; 
    public GameObject Power;

    
    void Update()
    {
        Move();
        Jump();
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
        if (Input.GetKeyDown(KeyCode.Space)&& isGrounded)
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

