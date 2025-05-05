using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatFormSpawner : MonoBehaviour
{
    public GameObject PlatPrefab;
    public Transform player;
    public float distanciaVertical = 2f;
    public float limiteX = 5f;
    public float margemAltura = 10f;
    private float alturaMaximaGerada = 0f;
    public float distanciaLimpeza = 15f;// Distância para começar a limpar blocos abaixo do jogador
    private List<GameObject> blocosGerados = new List<GameObject>();

    void Start()
    {
        alturaMaximaGerada = player.position.y + margemAltura;
        GerarBlocosAte(alturaMaximaGerada);
        
    }

    // Update is called once per frame
    void Update()
    {
        float alvo = player.position.y + margemAltura;

        if (alvo > alturaMaximaGerada)
        {
            GerarBlocosAte(alvo);
        }

        LimparBlocosAbaixoDoJogador();
    }
    void GerarBlocosAte(float alturaAlvo)
    {
        while (alturaMaximaGerada < alturaAlvo)
        {
            float posX = Random.Range(-limiteX, limiteX);
            Vector2 spawnPos = new Vector2(posX, alturaMaximaGerada);

            GameObject novoBloco = Instantiate(PlatPrefab, spawnPos, Quaternion.identity);
            blocosGerados.Add(novoBloco);

            alturaMaximaGerada += distanciaVertical;
        }
    }
    void LimparBlocosAbaixoDoJogador()
    {
        // Verifica todos os blocos gerados
        for (int i = blocosGerados.Count - 1; i >= 0; i--)
        {
            GameObject bloco = blocosGerados[i];

            if (bloco != null && bloco.transform.position.y < player.position.y - distanciaLimpeza)
            {
                Destroy(bloco);
                blocosGerados.RemoveAt(i);
            }         
        }
    }
}
