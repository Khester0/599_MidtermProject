using UnityEngine;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(DamageTaken))]
public class Skeleton_Enemy : MonoBehaviour
{

    public float walkspeed = 3f;
    public float walkStopRate = 0.5f;
    public DetectionZone attackZone;
    public DetectionZone cliffDetection;

    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Animator animator;
    DamageTaken damageTaken;

    public enum WalkableDirection {Right, Left};

    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;

    public WalkableDirection WalkDirection
    {
        get{return _walkDirection;}
        set{
            if(_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if(value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }else if(value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;}
    }

    public bool _hasTarget = false;

    public bool HasTarget 
        { get 
        {
            return _hasTarget;
        } 
        private set
        {
            _hasTarget = value;
            animator.SetBool("hasTarget", value);
        }}

        public bool CanMove
        {
            get
            {
                return animator.GetBool("canMove");
            }
        }

    public float AttackCooldown 
    {
        get
        {
            return animator.GetFloat("attackCoolDown");   

        }
        private set
        {
            animator.SetFloat("attackCoolDown", Mathf.Max(value, 0));
        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageTaken = GetComponent<DamageTaken>();
    }
    // Update is called once per frame
    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;

        if(AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
        
    }

    private void FixedUpdate()
    {
        if(touchingDirections.IsOnWall && touchingDirections.IsGrounded || cliffDetection.detectedColliders.Count == 0)
        {
            FlipDirection();
        }

        if(!damageTaken.LockVelocity)
        {
            if(CanMove)
                rb.linearVelocity = new Vector2 (walkspeed * walkDirectionVector.x, rb.linearVelocity.y);
            else
                rb.linearVelocity = new Vector2(Mathf.Lerp(rb.linearVelocity.x, 0, walkStopRate ), rb.linearVelocity.y);
        }
        
    }
    private void FlipDirection()
    {
        if(WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }else if(WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }else
        {
            Debug.LogError("Error WalkableDirection is not set to legal values of right or left.");
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        rb.linearVelocity = new Vector2(knockback.x, rb.linearVelocity.y + knockback.y);
    }
    
}
