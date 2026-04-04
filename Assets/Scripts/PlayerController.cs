using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float walkspeed = 5f;
    public float runspeed = 8f;
    Vector2 moveInput;

    public float CurrentMoveSpeed 
    {
        get
        {
            if(IsMoving)
            {
                if(IsRunning)
                {
                    return runspeed;
                }
                else
                {
                    return walkspeed;
                }
            }else
            {
                return 0;
            }
        }
    }

    [SerializeField]
    private bool _isMoving = false;

    public bool IsMoving {get 
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool("isMoving", value);

        }
      }
      
     [SerializeField]
     private bool _isRunning = false;

     public bool IsRunning
     {
         get
         {
             return _isRunning;
         }
         set
         {
             _isRunning = value;
             animator.SetBool("isRunning", value);
         }
     }

    Rigidbody2D rb;
    Animator animator;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2 (moveInput.x * CurrentMoveSpeed, rb.linearVelocity.y); 
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            IsRunning = true;

        }
        else if(context.canceled)
        {
            IsRunning = false;
        }
    }
}
