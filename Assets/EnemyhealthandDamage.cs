using UnityEngine;

public class EnemyhealthandDamage : MonoBehaviour
{
    private int currentHealth;
    public int Maxhealth = 50;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = Maxhealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("enemy die");
    }

    
}
