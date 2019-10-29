using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private const float gravity = -19.62f;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float m_verticalVelocity;
    [SerializeField] private float m_maxVerticalVelocity;

    private const float m_jumpEnableThresholdTime = .25f;
    private float m_jumpTimer;

    void Start()
    {
        m_verticalVelocity = 0;
        m_jumpTimer = 0;
    }

    void Update()
    {
        m_verticalVelocity += gravity * Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Space) && m_jumpTimer >= m_jumpEnableThresholdTime){
            m_verticalVelocity += jumpVelocity;
            m_jumpTimer = 0;
        }

        m_verticalVelocity = Mathf.Clamp(m_verticalVelocity, -m_maxVerticalVelocity, m_maxVerticalVelocity);
        transform.position += Vector3.up * m_verticalVelocity * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.9f, 3.9f), 0);
    
        m_jumpTimer += Time.deltaTime;
    }
}
