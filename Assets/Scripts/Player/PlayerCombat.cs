using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public LayerMask enemyLayers;

    public GameObject projectile;
    public Transform shootPoint;
    // public float shootRecoil = 0.1f;
    public float shootInterval = 0.2f;
    private float shootIntervalRemaining;

    public Transform blastPoint;
    public float blastRadius = 0.5f;
    public float blastDamage = 20f;
    // public float blastRecoil = 0.2f;
    public float blastInterval = 0.5f;
    private float blastIntervalRemaining;
    public AudioClip blastClip;

    private void Start()
    {
        this.blastIntervalRemaining = this.blastInterval;
    }

    void Update()
    {
        if (Input.GetButtonDown("Shoot") && CanAttack())
        {
            this.Shoot();
        }
        else if (Input.GetButtonDown("Blast") && CanAttack())
        {
            this.StartBlast();
        }
    }

    private bool CanAttack() => blastIntervalRemaining <= 0 && shootIntervalRemaining <= 0;

    private void FixedUpdate()
    {
        if (this.blastIntervalRemaining > 0)
            this.blastIntervalRemaining -= Time.deltaTime;
        if (this.shootIntervalRemaining > 0)
            this.shootIntervalRemaining -= Time.deltaTime;
    }

    private void Shoot()
    {
        this.shootIntervalRemaining = this.shootInterval;
        this.animator.SetTrigger("Shoot");

        var obj = Instantiate(projectile, shootPoint.position, shootPoint.rotation);
        // controller.Recoil(shootRecoil, controller.m_FacingRight ? Vector2.left : Vector2.right);
    }

    private void StartBlast()
    {
        this.blastIntervalRemaining = this.blastInterval;
        this.animator.SetTrigger("Blast");

        var audio = GetComponent<AudioSource>();
        audio.clip = blastClip;
        audio.Play();
    }

    void Blast()
    {
        var collisions = Physics2D.OverlapCircleAll(blastPoint.position, blastRadius, enemyLayers);

        foreach (var enemy in collisions)
        {
            enemy.GetComponent<Patrol>().TakeDamage(blastDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(blastPoint.position, blastRadius);
    }
}