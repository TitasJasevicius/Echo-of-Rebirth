using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public AudioManager audioManager;
    public bool isGrounded = true; 

    public float runSpeed = 40f;

    float horizontalMove = 0f;

    bool jumpPressed = false;
    bool jumpHeld = false;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            jumpPressed = true;
            jumpHeld = true;

            audioManager.PlaySFX(audioManager.jump); // well, you can spam this sound for now


        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) // while holding space
        {
            jumpHeld = true;
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)) // if no longer holding space
        {
            jumpHeld = false;
        }
    }

    void FixedUpdate()
    {
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
