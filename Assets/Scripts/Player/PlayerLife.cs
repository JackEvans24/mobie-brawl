using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public CharacterController2D controller;
    public GameManager gameManager;
    public Text healthValueText;
    public float maxHealth = 100f;
    private float currentHealth;
    public Transform respawnPosition;
    public float respawnDamage;

    void Start()
    {
        this.SetHealth(this.maxHealth);
    }

    private void SetHealth(float health)
    {
        this.currentHealth = health;

        this.healthValueText.text = Mathf.Max(this.currentHealth, 0).ToString();
        if (this.currentHealth <= 0)
            this.healthValueText.color = new Color(212, 2, 2);

        this.animator.SetFloat("Health", this.currentHealth);    }

    public void Respawn()
    {
        this.rb.position = this.respawnPosition.position;
        this.TakeDamage(respawnDamage);
    }

    public void TakeDamage(float damage)
    {
        this.SetHealth(this.currentHealth - damage);

        this.animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        this.GetComponent<CharacterController2D>().enabled = false;
        this.GetComponent<PlayerCombat>().enabled = false;
        foreach (var collider in this.GetComponents<Collider2D>()) {
            collider.enabled = false;
        }

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector2.zero;

        this.gameManager.GameOver();

        this.enabled = false;
    }
}
