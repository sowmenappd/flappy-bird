using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{

    public bool Started{
        get; private set;
    }
    public bool GameOver{
        get; private set;
    }

    public TextMeshProUGUI scoreText;

    public CanvasGroup hintPanel;
    public Transform gameOverPanel;
    public Transform menuPanel;

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
        GameOver = false;
        scoreText.gameObject.SetActive(false);
        Score = 0;
    }

    void Update(){
        if(!Started && !GameOver && Input.GetKeyDown(KeyCode.Space)){
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
        if(GameOver) return;

        GameOver = true;
        gameOverPanel.gameObject.SetActive(true);
        menuPanel.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        
        ProcessScore(Score);
    }

    private void ProcessScore(int Score){
        gameOverPanel.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().SetText(Score.ToString());
        int currentScore = PlayerPrefs.GetInt("Highscore", 0);
        if(Score > currentScore){
            PlayerPrefs.SetInt("Highscore", Score);
            gameOverPanel.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().SetText(Score.ToString());
            gameOverPanel.GetChild(2).GetChild(2).gameObject.SetActive(true);
        } else {
           gameOverPanel.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().SetText(currentScore.ToString()); 
        }
    }

    public void LoadMenu(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

}
