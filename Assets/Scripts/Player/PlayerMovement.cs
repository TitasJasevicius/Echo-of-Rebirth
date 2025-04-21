using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    public float runSpeed = 40f;

    float horizontalMove = 0f;

    bool jumpPressed = false;
    bool jumpHeld = false;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jumpPressed = true;
            jumpHeld = true;
        }

        if (Input.GetButton("Jump")) // while holding space
        {
            jumpHeld = true;
        }

        if (Input.GetButtonUp("Jump")) // if no longer holding space
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
}
