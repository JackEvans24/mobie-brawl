using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public CharacterController2D controller;
    public GameObject sprite;
    public GameManager gameManager;
    public Text healthValueText;

    public float maxHealth = 100f;
    private float currentHealth;

    public Transform respawnPosition;
    public float respawnDamage;

    public float recoveryTime = 1f;
    private float currentRecoveryTime;

    void Start()
    {
        this.SetHealth(this.maxHealth);
    }

    void Update()
    {
        if (currentRecoveryTime > 0)
            currentRecoveryTime -= Time.deltaTime;    
    }

    private void SetHealth(float health)
    {
        this.currentHealth = health;

        this.healthValueText.text = Mathf.Max(this.currentHealth, 0).ToString();
        if (this.currentHealth <= 0)
            this.healthValueText.color = new Color(212, 2, 2);

        this.animator.SetFloat("Health", this.currentHealth);
    }

    public void Respawn()
    {
        this.rb.position = this.respawnPosition.position;
        this.currentRecoveryTime = 0;
        this.TakeDamage(respawnDamage, Vector2.zero);
    }

    public void TakeDamage(float damage, Vector2 attackingBody)
    {
        if (currentRecoveryTime > 0) {
            return;
        }        

        this.SetHealth(this.currentHealth - damage);

        this.animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
            return;
        }
        else if (attackingBody != Vector2.zero)
        {
            gameObject.GetComponent<CharacterController2D>().Recoil(attackingBody);
        }

        this.currentRecoveryTime = recoveryTime;
        StartCoroutine(this.PlayRecoveryAnimation());
    }

    IEnumerator PlayRecoveryAnimation() {
        while (currentRecoveryTime > 0) {
            sprite.SetActive(!sprite.activeSelf);
            yield return new WaitForSecondsRealtime(0.1f);
        }
        
        sprite.SetActive(true); 
    }

    void Die()
    {
        this.GetComponent<CharacterController2D>().enabled = false;
        this.GetComponent<PlayerCombat>().enabled = false;
        foreach (var collider in this.GetComponents<Collider2D>()) {
            collider.enabled = false;
        }

        this.currentRecoveryTime = 0;
        sprite.SetActive(true); 

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector2.zero;

        this.gameManager.GameOver();

        this.enabled = false;
    }
}
