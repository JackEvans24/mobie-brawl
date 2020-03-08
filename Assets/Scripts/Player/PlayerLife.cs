using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public CharacterController2D controller;
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        this.currentHealth = this.maxHealth;
        this.animator.SetFloat("Health", this.currentHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        this.animator.SetTrigger("Hurt");
        this.animator.SetFloat("Health", this.currentHealth);

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

        StartCoroutine(GameManager.RestartInSeconds(5));

        this.enabled = false;
    }
}
