using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManeger : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI score;
    public TextMeshProUGUI highScore;

    private float alturaMaxima;
    private int pontos;
    private int recorde;

    void Start()
    {
        // Carrega o recorde salvo
        recorde = PlayerPrefs.GetInt("Recorde", 0);
        highScore.text = "Recorde: " + recorde.ToString();
    }

        void Update()
    {
       if (player.position.y > alturaMaxima)
        {
            alturaMaxima = player.position.y;
            pontos = Mathf.FloorToInt(alturaMaxima * 5); // 5 pontos por unidade de altura

            score.text = "Score: " + pontos.ToString();

            if (pontos > recorde)
            {
                recorde = pontos;
                highScore.text = "Recorde: " + recorde.ToString();
                PlayerPrefs.SetInt("Recorde", recorde);
            }
        } 
    }
}
