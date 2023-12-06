using UnityEngine;
using UnityEngine.UI;
public class GameSession : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;

    private void Update()
    {
        scoreText.text = GetScore().ToString();
        PlayerPrefs.SetInt("lastScore", score);
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
        if (score > PlayerPrefs.GetInt("highScore", 0))
        {
            PlayerPrefs.SetInt("highScore", score);
        }
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
