using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;

	[SerializeField] private float m_JumpForce = 40f;							// Amount of force added when the player jumps.
	[SerializeField] private float m_AntiJumpForce = 40f;						// Amount of force added when the player jumps.
	[SerializeField] private float m_JumpCancelCoefficient = 3f;					// Amount of force added when the player jumps.
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.

    public float runSpeed = 40f;

    private float horizontalMovement;
    private bool jump;
    private bool holdJump;

    public float recoilStrength;
	public float recoilJump;
	private bool recoilFromRight;
    public float recoilDuration;
	private float recoilDurationRemaining;

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	public bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
	
	void Update()
	{
		this.horizontalMovement = this.runSpeed * this.GetMovementCoefficient();
        this.jump = Input.GetButtonDown("Jump");
        this.holdJump = Input.GetButton("Jump");

        this.SetAnimationParameters();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
			}
		}

		if (recoilDurationRemaining <= 0)
			this.Move(this.horizontalMovement, this.jump, this.holdJump);
		else
			this.RecoilInternal();
	}


	public void Move(float move, bool jump, bool holdJump)
	{
		var yVelocity = rb.velocity.y;
		// If the player should jump...
		if (m_Grounded && jump)
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			yVelocity += m_JumpForce;
		}
		else if (yVelocity > 0 && !holdJump)
		{
			yVelocity -= m_JumpCancelCoefficient * m_AntiJumpForce;
		}
		else if (yVelocity > -2 * m_JumpForce)
		{
			yVelocity -= m_AntiJumpForce;
		}


		rb.velocity = new Vector2(0f, yVelocity);
		rb.velocity = new Vector2(move, yVelocity);

		// If the input is moving the player right and the player is facing left...
		if (move > 0 && !m_FacingRight)
		{
			// ... flip the player.
			Flip();
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (move < 0 && m_FacingRight)
		{
			// ... flip the player.
			Flip();
		}
	}

	public void Recoil(Vector2 recoilFrom)
	{
		recoilFromRight = recoilFrom.x - rb.position.x >= 0;

		if (recoilFromRight && !m_FacingRight || !recoilFromRight && m_FacingRight)
			Flip();
		
		recoilDurationRemaining = recoilDuration;
	}

	private void RecoilInternal()
	{
		rb.velocity = new Vector2(recoilFromRight ? -recoilStrength : recoilStrength, recoilJump);
		recoilDurationRemaining -= Time.deltaTime;
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Rotate the player completely on the y
		transform.Rotate(0f, 180f, 0f);
	}

	private float GetMovementCoefficient()
    {
        var coefficient = 0;
        
        var left = Input.GetButton("Left");
        var right = Input.GetButton("Right");
        
        if (left && !right)
            coefficient = -1;
        else if (right && !left)
            coefficient = 1;

        return coefficient;
    }

    private void SetAnimationParameters()
    {
        this.animator.SetFloat("Speed", Mathf.Abs(this.horizontalMovement));
        this.animator.SetFloat("VerticalSpeed", this.rb.velocity.y);
	}
}
