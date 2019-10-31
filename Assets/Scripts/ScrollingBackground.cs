using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField][Range(0f,0.7f)] private float scrollSpeed;
    private Renderer m_renderer;
    private ScoreController m_scoreController;
    private bool m_isMainMenu;

    void Start()
    {
        m_renderer = GetComponent<Renderer>();
        m_scoreController = ScoreController.Instance;
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Main Menu"){
            m_isMainMenu = true;
            GameObject.Find("Start Btn").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => 
                UnityEngine.SceneManagement.SceneManager.LoadScene(1)
            );
        }
    }

    void Update()
    {
        if(m_isMainMenu || !m_scoreController.Started || m_scoreController.GameOver) return;
        m_renderer.material.mainTextureOffset += Vector2.right * scrollSpeed * Time.deltaTime;        
    }
}
