using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchingDirections : MonoBehaviour
{
    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    Rigidbody2D rb;
    Animator animator;

    CapsuleCollider2D touchingcol;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    
    
    private bool _isGrounded;
        
    public bool IsGrounded {get
        {
            return _isGrounded;

        }private set
        {
            _isGrounded = value;
            animator.SetBool("isGrounded", value);
        }}

    private bool _isOnWall;
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        
    public bool IsOnWall {get
        {
            return _isOnWall;

        }private set
        {
            _isOnWall = value;
            animator.SetBool("isOnWall", value);
        }}
    

    private void Awake()
    {
        
        touchingcol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }
   

    // Update is called once per frame
    void FixedUpdate()
    {
        IsGrounded = touchingcol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnWall = touchingcol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
    }
}
