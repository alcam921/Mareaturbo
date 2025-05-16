using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject victoryPanel;
    public Text victoryText;
    private bool gameEnded = false;

    public void PlayerWon(int playerIndex)
    {
        if (gameEnded) return;

        gameEnded = true;
        victoryPanel.SetActive(true);
        victoryText.text = $"Jogador {playerIndex + 1} venceu!";
        Time.timeScale = 0f; // pausa o jogo
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
