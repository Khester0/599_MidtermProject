using UnityEngine;
using UnityEngine.Events;

public class Spikes : MonoBehaviour
{
    

    public float spikeDamage = 5;
    public float playerBounce = 10f;
    DamageTaken takenDamage;
    Animator animator;
    
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            DamageTaken playerHealth = collision.gameObject.GetComponent<DamageTaken>();
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            
            if(playerHealth != null)
            {
                playerHealth.Health -= spikeDamage;
                
                Debug.Log("player got damaged");

                if(rb != null)
                {
                    rb.linearVelocity = Vector2.zero;

                    rb.AddForce(Vector2.up * playerBounce, ForceMode2D.Impulse);

                    if(playerHealth.Health <= 0)
                    {
                        rb.linearVelocity = Vector2.zero;
                    }
                }  
                collision.gameObject.GetComponent<Animator>().SetTrigger("hit");
            }
        }
    }

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
