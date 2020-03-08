using System;
using System.Collections;
using UnityEngine;

public class Patrol : Enemy
{
    public Transform platformDetection;
    public float speed;
    public float chaseSpeed;
    public float groundDetectionDistance;
    public float obstacleDetectionDistance;
    public float playerDetectionDistance;
    public LayerMask groundLayer;
    public LayerMask playerLayer;
    public float waitTime;

    private float movement;
    private float directionCoefficient { get { return this.facingRight ? 1 : -1; }}
    private bool facingRight = true;
    private bool stopped = false;
    
    private Transform player;

    public GameObject calmEyes;
    public GameObject angryEyes;

    void Start() => base.OnStart();

    void Update()
    {
        base.OnUpdate();

        if (player == null)
        {
            CheckForPlayer();

            if (!recoiling && currentHealth > 0 && !stopped)
                this.CheckForObstacles();
        }

        this.movement = this.GetVelocity();
        this.animator.SetFloat("Speed", Mathf.Abs(movement));
    }

    private void CheckForPlayer()
    {
        RaycastHit2D playerRightInfo = Physics2D.Raycast(platformDetection.position, Vector2.right, playerDetectionDistance, playerLayer);
        RaycastHit2D playerLeftInfo = Physics2D.Raycast(platformDetection.position, Vector2.left, playerDetectionDistance, playerLayer);
        if (this.ValidPlayer(playerLeftInfo.collider))
        {
            this.player = playerLeftInfo.collider.transform;
        }
        else if (this.ValidPlayer(playerRightInfo.collider))
        {
            this.player = playerRightInfo.collider.transform;
        }

        if (player != null)
        {
            this.calmEyes.SetActive(false);
            this.angryEyes.SetActive(true);
        }
    }

    private bool ValidPlayer(Collider2D collider) => collider != false && collider.tag == "Player";

    private void CheckForObstacles()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(platformDetection.position, Vector2.down, groundDetectionDistance, groundLayer);
        RaycastHit2D obstacleInfo = Physics2D.Raycast(platformDetection.position, Vector2.right * directionCoefficient, obstacleDetectionDistance);

        if (groundInfo.collider == false || ValidObstacle(obstacleInfo.collider))
        {
            stopped = true;
            StartCoroutine(this.TurnAround());
        }
    }

    private bool ValidObstacle(Collider2D collider) => collider != false && collider.tag != "Player";

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movement, rb.velocity.y);
    }

    private float GetVelocity()
    {
        if (recoiling || currentHealth <= 0)
            return 0;

        if (player != null)
            return this.ChasePlayer();

        if (stopped)
            return 0;

        return speed * directionCoefficient;
    }

    private float ChasePlayer()
    {
        var playerDirection = player.position.x - rb.position.x;

        if (Math.Abs(playerDirection) <= 0.2)
            return 0;

        if ((facingRight && playerDirection < 0) || (!facingRight && playerDirection > 0))
            this.Flip();

        return chaseSpeed * (playerDirection > 0 ? 1 : -1);
    }

    private IEnumerator TurnAround()
    {
        yield return new WaitForSeconds(waitTime);

        if (currentHealth <= 0)
            yield break;

        this.Flip();
        stopped = false;
    }

    private void Flip()
    {
		facingRight = !facingRight;
		transform.Rotate(0f, 180f, 0f);
    }
}
