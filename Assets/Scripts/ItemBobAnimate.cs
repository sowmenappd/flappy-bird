using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBobAnimate : MonoBehaviour
{
    public float bobSpeed = 4f;

    public float maxBobDelta = 0.5f;
    private float m_bobDistance = 0.1f;

    private Vector3 m_startPos;
    private int dir = 1;

    void Start()
    {
        m_startPos = transform.position;
    }

    void Update()
    {
        m_bobDistance += Time.deltaTime * bobSpeed * dir;

        if(m_bobDistance > maxBobDelta) {
            dir = -dir;
            m_bobDistance = maxBobDelta - 0.01f;
        }

        if(m_bobDistance < -maxBobDelta) {
            dir = -dir;
            m_bobDistance = -maxBobDelta + 0.01f; 
        }
        transform.position = m_startPos + Vector3.up * m_bobDistance;
    }
}
