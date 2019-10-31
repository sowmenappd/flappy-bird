using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField][Range(0f,0.7f)] private float scrollSpeed;
    private Renderer m_renderer;
    private ScoreController m_scoreController;

    void Start()
    {
        m_renderer = GetComponent<Renderer>();
        m_scoreController = ScoreController.Instance;
        //m_renderer.material.shader = Shader.Find("Unlit/Texture");
    }

    void Update()
    {
        if(!m_scoreController.Started) return;
        m_renderer.material.mainTextureOffset += Vector2.right * scrollSpeed * Time.deltaTime;        
    }
}
