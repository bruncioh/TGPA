using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Movement m_MovementScript;
    [SerializeField]
    private CameraController m_CameraController;

    Controls_Player m_Controls;
    Controls_Player.PlayerMovementActions m_Movement;

    Vector2 m_HorizontalMovement;
    Vector2 m_MouseInput;


    private void Awake()
    {
        m_Controls = new Controls_Player();
        m_Movement = m_Controls.PlayerMovement;

        m_Movement.HorizontalMovement.performed += ctx => m_HorizontalMovement = ctx.ReadValue<Vector2>();
        m_Movement.Jump.performed += _ => m_MovementScript.OnJump();

        m_Movement.MouseX.performed += ctx => m_MouseInput.x = ctx.ReadValue<float>();
        m_Movement.MouseY.performed += ctx => m_MouseInput.y = ctx.ReadValue<float>();
    }

    private void Update()
    {
        m_MovementScript.Input(m_HorizontalMovement);
        m_CameraController.MouseInput(m_MouseInput);
    }

    private void OnEnable()
    {
        m_Controls.Enable();
    }

    private void OnDisable()
    {
        m_Controls.Disable();
    }
}
