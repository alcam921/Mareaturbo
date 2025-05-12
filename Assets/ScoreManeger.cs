using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public Transform player1;
    public Transform player2;

    public TextMeshProUGUI score1;
    public TextMeshProUGUI score2;
    public TextMeshProUGUI highScore1;
    public TextMeshProUGUI highScore2;

    private float alturaMaxima1 = 0f;
    private float alturaMaxima2 = 0f;

    private int pontos1 = 0;
    private int pontos2 = 0;

    private int recorde1;
    private int recorde2;

    void Start()
    {
        // Carrega os recordes salvos
        recorde1 = PlayerPrefs.GetInt("Recorde_Player1", 0);
        recorde2 = PlayerPrefs.GetInt("Recorde_Player2", 0);

        highScore1.text = "P1 Recorde: " + recorde1;
        highScore2.text = "P2 Recorde: " + recorde2;
    }

    void Update()
    {
        // Player 1
        if (player1.position.y > alturaMaxima1)
        {
            alturaMaxima1 = player1.position.y;
            pontos1 = Mathf.FloorToInt(alturaMaxima1 * 5); // 5 pontos por unidade de altura
            score1.text = "P1 Score: " + pontos1;

            if (pontos1 > recorde1)
            {
                recorde1 = pontos1;
                highScore1.text = "P1 Recorde: " + recorde1;
                PlayerPrefs.SetInt("Recorde_Player1", recorde1);
            }
        }

        // Player 2
        if (player2.position.y > alturaMaxima2)
        {
            alturaMaxima2 = player2.position.y;
            pontos2 = Mathf.FloorToInt(alturaMaxima2 * 5);
            score2.text = "P2 Score: " + pontos2;

            if (pontos2 > recorde2)
            {
                recorde2 = pontos2;
                highScore2.text = "P2 Recorde: " + recorde2;
                PlayerPrefs.SetInt("Recorde_Player2", recorde2);
            }
        }
    }
}
