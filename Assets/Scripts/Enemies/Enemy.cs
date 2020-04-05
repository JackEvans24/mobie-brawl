using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    private GameManager gameManager;

    protected bool recoiling = false;
    public float recoilTime = 20f;
    protected float recoilTimeRemaining;

    public float maxHealth = 100f;
    public float currentHealth;

    public float attackDamage = 10f;

    public RemoveFromGame removeScript;

    void Awake()
    {
        this.gameManager = (GameManager)Object.FindObjectOfType(typeof(GameManager));
    }

    protected void OnStart()
    {
        this.currentHealth = this.maxHealth;
        this.recoilTimeRemaining = recoilTime;

        this.animator.SetFloat("Health", currentHealth);
    }

    protected void OnUpdate()
    {
        this.animator.SetFloat("VerticalSpeed", this.rb.velocity.y);
        
        if (recoiling)
            this.CheckRecoil();
        else if (currentHealth <= 0)
        {
            this.Die();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerLife>().TakeDamage(attackDamage, rb.position);
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

        this.DieExternal();
    }

    public void DieExternal()
    {
        foreach (var collider in this.GetComponents<Collider2D>()) {
            collider.enabled = false;
        }

        this.gameManager.IncrementMurderCount();
        
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