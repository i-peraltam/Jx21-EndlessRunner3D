using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;

    private int score;
    private int highScore;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "Puntaje máximo: " + highScore;
    }

    void Update()
    {
        score = Mathf.FloorToInt(player.position.z);

        scoreText.text = "Puntaje: " + score;

        if (score > highScore)
        {
            highScore = score;

            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();

            highScoreText.text = "Puntaje máximo: " + highScore;
        }
    }
}