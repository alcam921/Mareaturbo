using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public float alturaQueda = 0f; // quantas unidades abaixo da câmera o jogador pode cair para morrer

    void Update()
    {
        float posCameraY = Camera.main.transform.position.y;

        if (transform.position.y < posCameraY - alturaQueda)
        {
            Morrer();
        }
    }
    void Morrer()
    {
        Debug.Log("morreu");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reinicia a cena atual
    }
}
