using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    private GameObject arrowPrefab;
    // Start is called before the first frame update
    void Start()
    {
        arrowPrefab = Resources.Load("arrow") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown (0))
        {
            GameObject newArrow = Instantiate(arrowPrefab) as GameObject;
            newArrow.transform.position = transform.position;
            Rigidbody2D rb = newArrow.GetComponent<Rigidbody2D>();
            rb.velocity = Camera.main.transform.forward * 30;
        }
    }
}
