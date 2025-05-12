using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatFormSpawner : MonoBehaviour
{
    public GameObject PlatPrefab;  // Prefab da plataforma a ser gerada
    public Transform player1;      // Referência ao jogador 1
    public Transform player2;      // Referência ao jogador 2
    public float distanciaVertical = 2f;  // Distância entre as plataformas geradas
    public float limiteX = 5f;    // Limite horizontal para a geração das plataformas
    public float margemAltura = 10f; // Distância extra para gerar plataformas acima da altura dos jogadores
    private float alturaMaximaGerada = 0f;  // A altura mais alta onde plataformas foram geradas
    public float destroiPlat = 15f; // Distância para começar a destruir plataformas abaixo dos jogadores
    private List<GameObject> blocosGerados = new List<GameObject>(); // Lista para armazenar as plataformas geradas

    // Método chamado quando o jogo começa
    void Start()
    {
        // Usa a maior altura entre os dois jogadores como ponto inicial
        float alturaInicial = Mathf.Max(player1.position.y, player2.position.y);

        // Define a altura máxima inicial mais uma margem
        alturaMaximaGerada = alturaInicial + margemAltura;

        // Gera plataformas até a altura inicial com margem
        GerarBlocosAte(alturaMaximaGerada);
    }

    // Método chamado a cada frame
    void Update()
    {
        // Obtém a maior altura entre os dois jogadores
        float alturaMaisAlta = Mathf.Max(player1.position.y, player2.position.y);

        // Define o alvo de altura que será atingido (maior altura dos jogadores + margem)
        float alvo = alturaMaisAlta + margemAltura;

        // Se o jogador estiver mais alto, gera novas plataformas até alcançar o novo alvo
        if (alvo > alturaMaximaGerada)
        {
            GerarBlocosAte(alvo);
        }

        // Limpa plataformas que estão abaixo dos jogadores
        LimparBlocosAbaixoDosJogadores();
    }

    // Método para gerar plataformas até uma altura alvo
    void GerarBlocosAte(float alturaAlvo)
    {
        // Gera plataformas enquanto a altura máxima gerada for menor que o alvo
        while (alturaMaximaGerada < alturaAlvo)
        {
            // Gera uma posição X aleatória dentro dos limites
            float posX = Random.Range(-limiteX, limiteX);

            // Define a posição de spawn da plataforma (somente no eixo Y baseado em alturaMaximaGerada)
            Vector2 spawnPos = new Vector2(posX, alturaMaximaGerada);

            // Cria a nova plataforma e a adiciona à lista de plataformas geradas
            GameObject novoBloco = Instantiate(PlatPrefab, spawnPos, Quaternion.identity);
            blocosGerados.Add(novoBloco);

            // Atualiza a altura máxima gerada, para a próxima plataforma
            alturaMaximaGerada += distanciaVertical;
        }
    }

    // Método para limpar as plataformas abaixo dos jogadores
    void LimparBlocosAbaixoDosJogadores()
    {
        // Usa a menor altura entre os dois jogadores como referência para destruir plataformas
        float menorAltura = Mathf.Min(player1.position.y, player2.position.y);

        // Verifica todas as plataformas geradas
        for (int i = blocosGerados.Count - 1; i >= 0; i--)
        {
            GameObject bloco = blocosGerados[i];

            // Se a plataforma está abaixo dos jogadores e já passou do limite de limpeza, destrói ela
            if (bloco != null && bloco.transform.position.y < menorAltura - destroiPlat)
            {
                Destroy(bloco); // Remove a plataforma da cena
                blocosGerados.RemoveAt(i); // Remove a plataforma da lista
            }
        }
    }
}
