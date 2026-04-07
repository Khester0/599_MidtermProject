using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackDamage = 10;
    public Vector2 knockback = Vector2.zero;
    Collider2D attackCollider;

    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageTaken damageTaken = collision.GetComponent<DamageTaken>();

        if(damageTaken != null)
        {
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            bool gotHit = damageTaken.Hit(attackDamage, deliveredKnockback);

            if(gotHit)
                Debug.Log(collision.name + " hit for " + attackDamage);
        }
    }
}
