using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; //Referência para o transform do jogador
    public float smoothSpeed = 0.125f; // Velocidade de suavisação do movimento da camera
    public Vector3 offset; //Distância entre a câmera e o jogador


    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        {
            
            Vector3 desideredPosition = target.position + offset;
            desideredPosition.z = transform.position.z; // manter a posição da câmera fixa
            Vector3 smoothedPosicion = Vector3.Lerp(transform.position, desideredPosition, smoothSpeed);
            transform.position = smoothedPosicion;
        }
    }
}
