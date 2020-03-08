using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public CharacterController2D controller;
    public float attackRadius = 0.5f;
    public float attackDamage = 20f;
    public float attackRecoil = 0.2f;

    void Update()
    {
        if (Input.GetButtonDown("Blast"))
        {
            this.StartBlast();
        }
    }

    private void StartBlast()
    {
        this.animator.SetTrigger("Blast");

        // controller.Recoil(attackRecoil, controller.m_FacingRight ? Vector2.left : Vector2.right);
    }

    void Blast()
    {
        var collisions = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayers);

        foreach (var enemy in collisions)
        {
            enemy.GetComponent<Patrol>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}