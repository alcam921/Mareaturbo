using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public float margemAbaixoCamera = 6f; // Quantos metros abaixo da c�mera o jogador pode cair

    void Update()
    {
        float posCameraY = Camera.main.transform.position.y;

        if (transform.position.y < posCameraY - margemAbaixoCamera)
        {
            Morrer();
        }
    }
    void Morrer()
    {
        Debug.Log("Voc� caiu!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reinicia a cena atual
    }
}
