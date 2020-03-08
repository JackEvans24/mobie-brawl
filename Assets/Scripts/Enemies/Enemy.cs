using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;

    protected bool recoiling = false;
    public float recoilTime = 20f;
    protected float recoilTimeRemaining;

    public float maxHealth = 100f;
    public float currentHealth;

    public float attackDamage = 10f;

    public RemoveFromGame removeScript;

    protected void OnStart()
    {
        this.currentHealth = this.maxHealth;
        this.recoilTimeRemaining = recoilTime;

        this.animator.SetFloat("Health", currentHealth);
    }

    protected void OnUpdate()
    {
        if (recoiling)
            this.CheckRecoil();
        else if (currentHealth <= 0)
        {
            this.Die();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerLife>().TakeDamage(attackDamage);
            other.GetComponent<CharacterController2D>().Recoil(rb.position);
        }
    }

    private void CheckRecoil()
    {
        this.recoilTimeRemaining--;
        if (this.recoilTimeRemaining <= 0) {
            this.recoilTimeRemaining = recoilTime;
            this.recoiling = false;
            return;
        }
    }

    void Die()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector2.zero;

        foreach (var collider in this.GetComponents<Collider2D>()) {
            collider.enabled = false;
        }
        
        StartCoroutine(this.removeScript.Remove());
        this.enabled = false;
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0)
            return;

        this.currentHealth -= damage;
        this.recoiling = true;

        this.animator.SetTrigger("Hurt");
        this.animator.SetFloat("Health", currentHealth);
    }
}