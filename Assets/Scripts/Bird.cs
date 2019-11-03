using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private const float gravity = -19.62f;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float m_maxVerticalVelocity;
    private float m_verticalVelocity;

    private const float m_jumpEnableThresholdTime = .25f;
    private float m_jumpTimer;

    private float m_lastKnownHeight;

    private float m_rotation;
    [SerializeField] private float m_torque;

    private bool m_canScore = true;
    
    private ScoreController m_scoreController;

    private const string flapSound = "wing";

    void Start()
    {
        m_verticalVelocity = 0;
        m_jumpTimer = 0;
        m_rotation = 0;
        m_scoreController = ScoreController.Instance;
    }

    void Update()
    {
        if(m_scoreController.Started){
            m_verticalVelocity += gravity * Time.deltaTime;
            m_verticalVelocity = Mathf.Clamp(m_verticalVelocity, -m_maxVerticalVelocity, m_maxVerticalVelocity);
            transform.position += Vector3.up * m_verticalVelocity * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.9f, 3.9f), 0);

            m_rotation += ((transform.position.y > m_lastKnownHeight) ? 8 : -4f) * Time.deltaTime * m_torque;
            m_rotation = Mathf.Clamp(m_rotation, -89, 30);
            transform.rotation = Quaternion.Euler(0, 0, m_rotation);
            m_lastKnownHeight = transform.position.y;
        }
        else 
            return;
        
        if(m_scoreController.GameOver) {
            return;
        }
        if(Input.GetKeyDown(KeyCode.Space) && m_jumpTimer >= m_jumpEnableThresholdTime){
            AudioManager.Instance.Play(flapSound, true);
            m_verticalVelocity += jumpVelocity;
            m_jumpTimer = 0;
        }
        m_jumpTimer += Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.collider.tag == "Respawn"){
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.GetChild(0).GetComponent<Animator>().enabled = false;
            if(!m_scoreController.GameOver){
                AudioManager.Instance.Play("hit", true);
            }
            m_scoreController.EndGame();
            this.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Respawn"){
            transform.GetChild(0).GetComponent<Animator>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 1f;
            if(!m_scoreController.GameOver){
                AudioManager.Instance.Play("hit", true);
            }
            m_scoreController.EndGame(true);
        }
    }
}
