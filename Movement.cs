using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float m_MoveSpeed = 10f;
    public float m_JumpHeight = 1f;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0f, 0f, Time.deltaTime * m_MoveSpeed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0f, 0f, Time.deltaTime * -m_MoveSpeed);

        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Time.deltaTime * m_MoveSpeed, 0f, 0f);

        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Time.deltaTime * -m_MoveSpeed, 0f, 0f);

        }

        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(0f, Time.deltaTime * m_MoveSpeed, 0f);

        }



    }
}
