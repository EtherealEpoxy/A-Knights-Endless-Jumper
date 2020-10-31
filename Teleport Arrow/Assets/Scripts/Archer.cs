using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    public Camera cam1;
    public Rigidbody2D rb;
    public BoxCollider2D bc;
    public Vector3 pos { get { return transform.position; } }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
    }

    public void Push(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    public void ActivateRb()
    {
        rb.isKinematic = false;
    }

    public void DeactivateRb()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
        rb.isKinematic = false;
    }

    private void FixedUpdate()
    {
        if (rb.position.y < (cam1.transform.position.y - 11.3))
        {
            FindObjectOfType<GameManager>().GameOver();
        }
    }
}
