using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; //Refer�ncia para o transform do jogador
    public float smoothSpeed = 0.125f; // Velocidade de suavisa��o do movimento da camera
    public Vector3 offset; //Dist�ncia entre a c�mera e o jogador


    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        {
            
            Vector3 desideredPosition = target.position + offset;
            desideredPosition.z = transform.position.z; // manter a posi��o da c�mera fixa
            Vector3 smoothedPosicion = Vector3.Lerp(transform.position, desideredPosition, smoothSpeed);
            transform.position = smoothedPosicion;
        }
    }
}
