using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float m_SensitivityX = 5f;
    [SerializeField]
    private float m_SensitivityY = 0.5f;
    [SerializeField]
    private Transform m_Camera;
    [SerializeField]
     private float m_xClamp = 85f;
    float m_XRotation = 0f;
    float m_MouseX;
    float m_MouseY;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, m_MouseX * Time.deltaTime);

        m_XRotation -= m_MouseY;
        m_XRotation = Mathf.Clamp(m_XRotation, -m_xClamp, m_xClamp);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = m_XRotation;
        m_Camera.eulerAngles = targetRotation;
    }

    public void MouseInput(Vector2 mouseInput)
    {
        m_MouseX = mouseInput.x * m_SensitivityX;
        m_MouseY = mouseInput.y * m_SensitivityY;
    }
}
