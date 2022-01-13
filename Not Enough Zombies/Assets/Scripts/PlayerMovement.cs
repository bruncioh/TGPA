using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController playerController;

    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;
    bool isGrounded;

    Vector3 velocity;
    public float gravity = -9.81f;
    public float playerSpeed = 12.0f;
    public float jumpHeight = 3.0f;

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckGround();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        
        playerController.Move(move * playerSpeed * Time.deltaTime);
    }

    private void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //if grounded reset velocity
        if (isGrounded && velocity.y < 0) { velocity.y = 0f; }

        if (Input.GetButtonDown("Jump") && isGrounded) { velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity); }

        velocity.y += gravity * Time.deltaTime;

        playerController.Move(velocity * Time.deltaTime);
    }
}
