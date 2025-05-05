using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalTeleporter : MonoBehaviour
{
    public float minX = -20.14f;
    public float maxX = 19.92f;
        
        void Update()
    {
        Vector3 pos = transform.position;

        if (pos.x > maxX)
            pos.x = minX;
        else if (pos.x < minX)
            pos.x = maxX;

        transform.position = pos;
    }
}
