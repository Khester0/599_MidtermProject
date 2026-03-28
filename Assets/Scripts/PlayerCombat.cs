using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform AttackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemylayers;
    public int attackDamage = 10;
    public float attackRate = 2f;
    public float nextattackTime = 0f;


    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextattackTime)
        {
            if(Input.GetKeyDown(KeyCode.X))
            {
                Attack();
                nextattackTime = Time.time + 1f / attackRate;
            }
        }

        
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        Collider2D[] hitenemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemylayers);
        
        foreach(Collider2D enemy in hitenemies)
        {
            enemy.GetComponent<EnemyhealthandDamage>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if(AttackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
    }
}
