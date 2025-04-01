using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform[] target; //Refer�ncia para o transform do jogador
    public float smoothSpeed = 0.125f; // Velocidade de suavisa��o do movimento da camera
    public Vector3 offset; //Dist�ncia entre a c�mera e o jogador
    private Transform _higherPlayer;

    // Start is called before the first frame update
    void Start()
    {
        _higherPlayer = target[0];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        {
            
            Vector3 desideredPosition = _higherPlayer.position + offset;
            desideredPosition.z = transform.position.z; // manter a posi��o da c�mera fixa
            Vector3 smoothedPosicion = Vector3.Lerp(transform.position, desideredPosition, smoothSpeed);
            transform.position = smoothedPosicion;
        }
    }
    public void SetHigherPlayer()
    {
        _higherPlayer = OrdenacaoBubbleSort(target)[0];
    }
    public static Transform[] OrdenacaoBubbleSort(Transform[] vetor)
    {
        Transform[] newList = vetor;
        float tamanho = newList.Length;
        float comparacoes = 0;
        float trocas = 0;

        for (float i = tamanho - 1; i >= 1; i--)
        {
            for (int j = 0; j < i; j++)
            {
                comparacoes++;
                if (newList[j].position.y > newList[j + 1].position.y)
                {
                    Transform aux = newList[j];
                    newList[j] = newList[j + 1];
                    newList[j + 1] = aux;
                    trocas++;
                }
            }
        }
        return vetor;
    }
}
