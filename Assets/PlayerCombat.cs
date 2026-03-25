using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform AttackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemylayers;
    public int attackDamage = 10;


    // Update is called once per frame
    void Update()
    {
        

        if(Input.GetKeyDown(KeyCode.X))
        {
            Attack();
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
