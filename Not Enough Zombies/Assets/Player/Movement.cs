using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    CharacterController m_Controller;
    [SerializeField]
    private float m_speed;
    [SerializeField]
    private float m_gravity = -9.8f;
    [SerializeField]
    LayerMask m_Ground;
    [SerializeField]
    private float m_JumpHeight;
    bool m_Jumping;
    bool m_Grounded;
    Vector3 m_VerticalVelocity = Vector3.zero;
    Vector2 m_HorizontalMovement;

    private void Update()
    {
        m_Grounded = Physics.CheckSphere(transform.position, 0.1f, m_Ground);
        if (m_Grounded)
        {
            m_VerticalVelocity.y = 0f;
        }

        Vector3 horizontalVelocity = (transform.right * m_HorizontalMovement.x + transform.forward * m_HorizontalMovement.y) * m_speed;
        m_Controller.Move(horizontalVelocity * Time.deltaTime);

        if (m_Grounded)
        {
            if (m_Jumping)
            {
                m_VerticalVelocity.y = Mathf.Sqrt(2f * m_JumpHeight * m_gravity);
            }
            m_Jumping = false;
        }

        m_VerticalVelocity.y += m_gravity * Time.deltaTime;
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
}
