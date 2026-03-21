using UnityEngine;
using System;

public class PlayerControl : MonoBehaviour
{

	public float speed = 5f;
    public float jumpForce = 30f; 

    private Rigidbody2D rb;
    private float moveX;
    private bool isGrounded ;
    bool isFacingRight;
    private Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");

        FlipSprite();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
            animator.SetBool("isJumping", !isGrounded);
        }
    }

    void FlipSprite()
    {
        if(isFacingRight && moveX < 0f || !isFacingRight && moveX > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2 (moveX * speed, rb.linearVelocity.y);
        animator.SetFloat("xVelocity", Math.Abs(rb.linearVelocity.x));
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isGrounded = false;
        animator.SetBool("isJumping", isGrounded);
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = true;
        animator.SetBool("isJumping", !isGrounded);
    }
     
     
   
}
