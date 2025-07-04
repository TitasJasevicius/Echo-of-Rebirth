using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 10f;                            // Initial jump velocity
    [SerializeField] private float m_FallMultiplier = 2.5f;                      // Multiplier for faster falling
    [SerializeField] private float m_LowJumpMultiplier = 2f;                     // Multiplier for shorter jumps
    [Range(0, .3f)][SerializeField] private float m_MovementSmoothing = .05f;    // How much to smooth out the movement
    [SerializeField] private LayerMask m_WhatIsGround;                           // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                            // A position marking where to check if the player is grounded.

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // Check if the player is grounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }

    public void Move(float move, bool jumpPressed, bool jumpHeld)
    {
        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.linearVelocity.y);
        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.linearVelocity = Vector3.SmoothDamp(m_Rigidbody2D.linearVelocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        // Handle sprite flipping
        if (move > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (move < 0 && m_FacingRight)
        {
            Flip();
        }

        // Handle jumping
        if (jumpPressed && m_Grounded)
        {
            m_Grounded = false;
            m_Rigidbody2D.linearVelocity = new Vector2(m_Rigidbody2D.linearVelocity.x, m_JumpForce);
        }

        // Adjust gravity for variable jump height
        if (m_Rigidbody2D.linearVelocity.y < 0)
        {
            // Falling down
            m_Rigidbody2D.linearVelocity += Vector2.up * Physics2D.gravity.y * (m_FallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (m_Rigidbody2D.linearVelocity.y > 0 && !jumpHeld)
        {
            // Jumping up but jump button released
            m_Rigidbody2D.linearVelocity += Vector2.up * Physics2D.gravity.y * (m_LowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
