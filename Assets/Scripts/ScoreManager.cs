// Author: Fatima Nadeem - (Proper comments pending)

using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    static public ScoreManager instance;

    public Text highScoreText;
    public Text scoreText;

    int score;
    int highScore;

    void Start()
    {   
        if (instance == null)
        {
            instance = this;
        }

        SetHighScore(0); //temporary, must remove

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "Highscore: " + highScore.ToString();

        //ResetScore();
    }

    public void SetHighScore(int newHighScore)
    {  
        PlayerPrefs.SetInt("HighScore", newHighScore);
        highScore = newHighScore;
        highScoreText.text = "Highscore: " + newHighScore.ToString();
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = "Score: " + score.ToString();
    }

    public void AddScore(int increment)
    {
        score += increment;
        scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateHighScore()
    {
        if (score > highScore)
        {
            SetHighScore(score);
            // celebratory animation around score to show new highscore
            // celebratory sound ~~~~~
        }
    }
}