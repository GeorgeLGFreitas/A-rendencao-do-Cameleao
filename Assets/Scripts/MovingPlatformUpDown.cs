using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformUpDown : MonoBehaviour
{
    private float useSpeed;
    public float directionSpeed = 10f;
    float origY;
    public float distance = 10f;

    void Start()
    {
        origY = transform.position.y;
        useSpeed = -directionSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (origY - transform.position.y > distance)
        {
            useSpeed = directionSpeed;
        }
        else if (origY - transform.position.y < -distance)
        {
            useSpeed = -directionSpeed;
        }
        transform.Translate(0, useSpeed * Time.deltaTime, 0);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(null);
        }
    }
}
