using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public AudioManager audioManager;
    public bool isGrounded = true;
    private bool wasGrounded = true;

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

    public bool inputLocked = false;
  private bool jumpSoundPlayed = false;
  public bool dashSoundPlayed = false;

  void Update()
    {

        if (inputLocked || !enabled)
        {
            animator.SetFloat("Speed", 0);
            return;
        }

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
 
        

      animator.SetBool("IsJumping", true);
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            jumpHeld = false;
            animator.SetBool("IsJumping", false);
        }

        // Handle dash input
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0f)
        {
            isDashing = true;
            dashTimeLeft = dashDuration;
            dashCooldownTimer = dashCooldown;
        if (!dashSoundPlayed)
        {
          audioManager.PlaySFX(audioManager.dash); // Play dash sound ONCE
          dashSoundPlayed = true;
        }
        else
        {
       
          dashSoundPlayed = false; // Reset dash sound flag after dash ends
        }
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
        dashSoundPlayed = false; // Reset dash sound flag after dash ends
        
      }
        }
        animator.SetBool("IsDashing", isDashing);

    bool isJumping = animator.GetBool("IsJumping"); // or check Animator state directly

    if (isJumping && !jumpSoundPlayed)
    {
      audioManager.PlaySFX(audioManager.jump);
      jumpSoundPlayed = true;
    }
    else if (!isJumping)
    {
      jumpSoundPlayed = false; // Reset when not jumping, so next jump can play sound
    }
  }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    void FixedUpdate()
    {
        if (inputLocked || !enabled)
            return;

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
