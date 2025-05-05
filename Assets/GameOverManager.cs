using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI score;
    public TextMeshProUGUI highScore;
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScore;
    public TextMeshProUGUI finalHighScore;
    private float alturaMaxima;
    private int pontos;
    private int recorde;
    private bool gameOver = false;
    void Start()
    {
        recorde = PlayerPrefs.GetInt("Recorde", 0);
        highScore.text = "Recorde: " + recorde.ToString();
        gameOverPanel.SetActive(false);
    }

    
    void Update()
    {
        if (gameOver) return;

        if (player.position.y > alturaMaxima)
        {
            alturaMaxima = player.position.y;
            pontos = Mathf.FloorToInt(alturaMaxima * 10);
            score.text = "Score: " + pontos.ToString();

            if (pontos > recorde)
            {
                recorde = pontos;
                highScore.text = "Recorde: " + recorde.ToString();
                PlayerPrefs.SetInt("Recorde", recorde);
            }
        }

        // Detecta se o jogador caiu
        if (player.position.y < Camera.main.transform.position.y - 9.9)
        {
            AtivarGameOver();
        }
    }
    void AtivarGameOver()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);
        finalScore.text = "Score Final: " + pontos;
        finalHighScore.text = "Recorde: " + recorde;
    }
    public void ReiniciarJogo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
