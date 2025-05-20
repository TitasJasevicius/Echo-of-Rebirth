using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public AudioManager audioManager;
    public bool isGrounded = true;

    public Animator animator;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    private int direction = 0; // 0 = right, 1 = left

    bool jumpPressed = false;
    bool jumpHeld = false;

    // Dash variables
    public float dashSpeed = 80f;          // Maximum speed during dash
    public float dashDuration = 0.2f;      // Duration of the dash in seconds
    public float dashCooldown = 1f;        // Cooldown time between dashes
    private float dashTimeLeft = 0f;       // Time left for the current dash
    private float dashCooldownTimer = 0f;  // Cooldown timer
    private bool isDashing = false;

    void Update()
    {

        // Handle horizontal movement input
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        // Update direction
        if (horizontalMove > 0)
        {
            direction = 0; // Right
        }
        else if (horizontalMove < 0)
        {
            direction = 1; // Left
        }

        // Decrease the cooldown timer
        if (dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        animator.SetInteger("Direction", direction);

        // Handle jump input
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            jumpPressed = true;
            jumpHeld = true;

            audioManager.PlaySFX(audioManager.jump); // You can spam this sound for now
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            jumpHeld = false;
        }

        // Handle dash input
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0f)
        {
            isDashing = true;
            dashTimeLeft = dashDuration;
            dashCooldownTimer = dashCooldown;
        }

        // Handle dashing
        if (isDashing)
        {
            if (dashTimeLeft > 0f)
            {
                dashTimeLeft -= Time.deltaTime;
                float dashProgress = (dashDuration - dashTimeLeft) / dashDuration;
                float speedMultiplier = Mathf.Sin(dashProgress * Mathf.PI);
                float dashSpeedAdjusted = Mathf.Lerp(runSpeed, dashSpeed, speedMultiplier);
                horizontalMove = Input.GetAxisRaw("Horizontal") * dashSpeedAdjusted;
            }
            else
            {
                isDashing = false;
            }
        }
    }

    void FixedUpdate()
    {
        // Apply movement
        controller.Move(horizontalMove * Time.fixedDeltaTime, jumpPressed, jumpHeld);
        jumpPressed = false;
    }

    public void IncreaseMovementSpeed(float value)
    {
        runSpeed += value;
        Debug.Log("Movement speed increased by " + value);
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
}
