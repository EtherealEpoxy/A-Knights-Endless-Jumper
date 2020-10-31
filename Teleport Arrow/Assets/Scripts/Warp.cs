using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (transform.position.x <= -7.7f)
        {
            transform.position = new Vector3(7.7f, transform.position.y, transform.position.z);

        }
        
        else if (transform.position.x >= 7.7f)
        {
            transform.position = new Vector3(-7.7f, transform.position.y, transform.position.z);
        }

    }
}
