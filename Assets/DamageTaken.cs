using UnityEngine;
using UnityEngine.Events;

public class DamageTaken : MonoBehaviour
{
    Animator animator;
    public UnityEvent<float, Vector2> damageableHit; 

    [SerializeField]
    private float _maxHealth = 100; 

    public float MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }
    
    [SerializeField]
    private float _health = 100;

    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;

            if(_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;

    public bool IsAlive
    {
        get
        {
            return _isAlive;

        }
        set
        {
            _isAlive = value;
            animator.SetBool("isAlive", value);
            Debug.Log("isalive set" + value);
        }
    }

    public bool LockVelocity
    {
        get
        {
            return animator.GetBool("lockVelocity");
        }
        set
        {
            animator.SetBool("lockVelocity", value);
        }
        
    }

    private bool isInvincible = false;
    private float timeSinceHit = 0;
    private float invinsibilityTime = 0.25f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public bool Hit(float damage, Vector2 knockback)
    {
        if(IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;
            
            animator.SetTrigger("hit");   
            LockVelocity = true;
            damageableHit?.Invoke(damage, knockback);

            return true;
        }

        return false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isInvincible)
        {
            if(timeSinceHit > invinsibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            
            timeSinceHit += Time.deltaTime;
        }

        
        
    }
}
