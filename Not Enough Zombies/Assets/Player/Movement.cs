using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    CharacterController m_Controller;
    [SerializeField]
    private float m_Speed;
    [SerializeField]
    private float m_RunSpeed;
    [SerializeField]
    private float m_Gravity = -9.8f;
    [SerializeField]
    LayerMask m_Ground;
    [SerializeField]
    private float m_JumpHeight;
    [SerializeField]
    private float m_GroundDetection = 0.2f;
    bool m_Jumping;
    bool m_Grounded;
    private float m_WalkSpeed;
    Vector3 m_VerticalVelocity = Vector3.zero;
    Vector2 m_HorizontalMovement;

    private void Awake()
    {
        m_WalkSpeed = m_Speed;
    }

    private void FixedUpdate()
    { 
        m_Grounded = Physics.CheckSphere(transform.position, m_GroundDetection , m_Ground);
        if (m_Grounded)
        {
            m_VerticalVelocity.y = 0f;
        }

        Vector3 horizontalVelocity = (transform.right * m_HorizontalMovement.x + transform.forward * m_HorizontalMovement.y) * m_Speed;
        m_Controller.Move(horizontalVelocity * Time.deltaTime);

        if (m_Grounded)
        {
            if (m_Jumping)
            {
                m_VerticalVelocity.y = Mathf.Sqrt(2f * m_JumpHeight * m_Gravity);
            }
            m_Jumping = false;
        }

        m_VerticalVelocity.y += m_Gravity * Time.deltaTime;
        m_Controller.Move(m_VerticalVelocity * Time.deltaTime);
    }

    public void Input(Vector2 horizontalInput)
    {
        m_HorizontalMovement = horizontalInput;
    }

    public void OnJump()
    {
        m_Jumping = true;
    }

    public void OnSprint()
    {
        if (m_Grounded)
        {
            m_Speed = m_RunSpeed;
        }
    }
    public void OnWalk()
    {
        m_Speed = m_WalkSpeed;
    }
}
