using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour
{
    float platformSpeed = 2f;
    bool endPoint;

    float startPoint;
    float endPointY;

    public int unitsToMove = 2;

    
    
    void Start()
    {
        startPoint = transform.position.y;

        endPointY = startPoint + unitsToMove;

    }


    void Update()
    {
        if (endPoint)
        {
            transform.position += Vector3.up * platformSpeed * Time.deltaTime;
        }

        else
        {
            transform.position -= Vector3.up * platformSpeed * Time.deltaTime;
        }

        if (transform.position.y >= endPointY)
        {
            endPoint = false;
        }

        if (transform.position.y <= startPoint)
        {
            endPoint = true;
        }
    }
}
