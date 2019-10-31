using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{

    public bool Started{
        get; private set;
    }

    public TextMeshProUGUI scoreText;

    public CanvasGroup hintPanel;

    private static ScoreController m_instance;
    public static ScoreController Instance{
        get {
            return m_instance;
        }
    }

    public int Score{
        get; private set;
    }

    void Awake(){
        m_instance = this;
    }

    void Start()
    {
        Started = false;
        scoreText.gameObject.SetActive(false);
        Score = 0;
    }

    void Update(){
        if(!Started && Input.GetKeyDown(KeyCode.Space)){
            Started = true;
        } else if(Started){
            hintPanel.alpha -= Time.deltaTime * 3f;
        }
    }

    public void UpdateScore(){
        ++Score;
        if(Score == 1){
            scoreText.gameObject.SetActive(true);
        }
        scoreText.text = Score.ToString();
    }

    public void EndGame(){
        Started = false;
        print("game over");
        //more ui to show
    }

}
