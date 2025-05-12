using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatFormSpawner : MonoBehaviour
{
    public GameObject PlatPrefab;  // Prefab da plataforma a ser gerada
    public Transform player1;      // Refer�ncia ao jogador 1
    public Transform player2;      // Refer�ncia ao jogador 2
    public float distanciaVertical = 2f;  // Dist�ncia entre as plataformas geradas
    public float limiteX = 5f;    // Limite horizontal para a gera��o das plataformas
    public float margemAltura = 10f; // Dist�ncia extra para gerar plataformas acima da altura dos jogadores
    private float alturaMaximaGerada = 0f;  // A altura mais alta onde plataformas foram geradas
    public float destroiPlat = 15f; // Dist�ncia para come�ar a destruir plataformas abaixo dos jogadores
    private List<GameObject> blocosGerados = new List<GameObject>(); // Lista para armazenar as plataformas geradas

    // M�todo chamado quando o jogo come�a
    void Start()
    {
        // Usa a maior altura entre os dois jogadores como ponto inicial
        float alturaInicial = Mathf.Max(player1.position.y, player2.position.y);

        // Define a altura m�xima inicial mais uma margem
        alturaMaximaGerada = alturaInicial + margemAltura;

        // Gera plataformas at� a altura inicial com margem
        GerarBlocosAte(alturaMaximaGerada);
    }

    // M�todo chamado a cada frame
    void Update()
    {
        // Obt�m a maior altura entre os dois jogadores
        float alturaMaisAlta = Mathf.Max(player1.position.y, player2.position.y);

        // Define o alvo de altura que ser� atingido (maior altura dos jogadores + margem)
        float alvo = alturaMaisAlta + margemAltura;

        // Se o jogador estiver mais alto, gera novas plataformas at� alcan�ar o novo alvo
        if (alvo > alturaMaximaGerada)
        {
            GerarBlocosAte(alvo);
        }

        // Limpa plataformas que est�o abaixo dos jogadores
        LimparBlocosAbaixoDosJogadores();
    }

    // M�todo para gerar plataformas at� uma altura alvo
    void GerarBlocosAte(float alturaAlvo)
    {
        // Gera plataformas enquanto a altura m�xima gerada for menor que o alvo
        while (alturaMaximaGerada < alturaAlvo)
        {
            // Gera uma posi��o X aleat�ria dentro dos limites
            float posX = Random.Range(-limiteX, limiteX);

            // Define a posi��o de spawn da plataforma (somente no eixo Y baseado em alturaMaximaGerada)
            Vector2 spawnPos = new Vector2(posX, alturaMaximaGerada);

            // Cria a nova plataforma e a adiciona � lista de plataformas geradas
            GameObject novoBloco = Instantiate(PlatPrefab, spawnPos, Quaternion.identity);
            blocosGerados.Add(novoBloco);

            // Atualiza a altura m�xima gerada, para a pr�xima plataforma
            alturaMaximaGerada += distanciaVertical;
        }
    }

    // M�todo para limpar as plataformas abaixo dos jogadores
    void LimparBlocosAbaixoDosJogadores()
    {
        // Usa a menor altura entre os dois jogadores como refer�ncia para destruir plataformas
        float menorAltura = Mathf.Min(player1.position.y, player2.position.y);

        // Verifica todas as plataformas geradas
        for (int i = blocosGerados.Count - 1; i >= 0; i--)
        {
            GameObject bloco = blocosGerados[i];

            // Se a plataforma est� abaixo dos jogadores e j� passou do limite de limpeza, destr�i ela
            if (bloco != null && bloco.transform.position.y < menorAltura - destroiPlat)
            {
                Destroy(bloco); // Remove a plataforma da cena
                blocosGerados.RemoveAt(i); // Remove a plataforma da lista
            }
        }
    }
}
