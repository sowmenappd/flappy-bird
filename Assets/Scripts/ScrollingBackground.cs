using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField][Range(0f,0.7f)] private float scrollSpeed;
    private Renderer m_renderer;
    private ScoreController m_scoreController;
    private bool m_isMainMenu = false;

    private GameObject screenFade;

    void Awake(){
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += (sc, mode) => {
            if(sc.name == "Game"){
                
            } else {
                m_isMainMenu = true;
                screenFade = GameObject.Find("Screen fade");
                GameObject.Find("Start Btn").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
                AudioManager.Instance.Play("swoosh", true);
                StartCoroutine(FadeToBlackAndLoadGame());
                });
                DontDestroyOnLoad(screenFade);
            }
        };
    }
    void Start()
    {
        m_renderer = GetComponent<Renderer>();
        m_scoreController = ScoreController.Instance;

        UnityEngine.SceneManagement.SceneManager.sceneLoaded += (sc, mode) => {
            if(sc.name == "Game"){
                if(!screenFade){
                    print("lalalala");
                    screenFade = GameObject.Find("Screen fade");
                    screenFade.GetComponent<Animator>().SetTrigger("out");
                }
            }
        };
    }

    void Update()
    {
        if(m_isMainMenu || !m_scoreController || !m_scoreController.Started || m_scoreController.GameOver) return;
        m_renderer.material.mainTextureOffset += Vector2.right * scrollSpeed * Time.deltaTime;        
    }

    private IEnumerator FadeToBlackAndLoadGame(){
        print("here");
        screenFade.GetComponent<Animator>().SetTrigger("in");
        yield return new WaitForSeconds(screenFade.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
