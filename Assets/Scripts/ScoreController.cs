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
        StartCoroutine(AnimateScoreCountUp(Score));
        int currentScore = PlayerPrefs.GetInt("Highscore", 0);
        if(Score > currentScore){
            PlayerPrefs.SetInt("Highscore", Score);
            gameOverPanel.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().SetText(Score.ToString()); 
            gameOverPanel.GetChild(2).GetChild(2).gameObject.SetActive(true);
        } else {
           gameOverPanel.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().SetText(currentScore.ToString()); 
        }
    }

    private IEnumerator AnimateScoreCountUp(int Score){
        int score = 0;
        float speed1 = 0.01f;
        float speed2 = 0.02f; 
        float speedF = 10f * speed1; 
        float per = speed1;
        float progress = 0f;

        yield return new WaitForSeconds(1.5f);
        while(score < Score){
            ++score;
            progress = score * 1f / Score;
            per = (progress < 0.5f) ? speed1 : (progress < 0.85f ) ? speed2 : speedF;
            gameOverPanel.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().SetText(score.ToString());
            yield return new WaitForSeconds(per);
            print(per);
        }
    }

    public void LoadMenu(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

}
