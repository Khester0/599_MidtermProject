using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    Rigidbody2D rb;
    
    public ContactFilter2D castFilter;
    CapsuleCollider2D touchingcol;
    Animator animator;

    public float grounddistance = 0.05f;

    private bool _isGrounded;

    RaycastHit2D[] groundhits = new RaycastHit2D[5];

    public bool IsGrounded {get{
            return _isGrounded;
        } private set{
            _isGrounded = value; 
            animator.SetBool("IsGrounded", value);
        
        } }

    private void Awake()
    {
        touchingcol = GetComponent<CapsuleCollider2D>();        
    }
  

    // Update is called once per frame
    void FixedUpdate()
    {
        IsGrounded = touchingcol.Cast(Vector2.down, castFilter, groundhits, grounddistance ) > 0; 
        
    }
}
