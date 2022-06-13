using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformLeftRight : MonoBehaviour
{
    private float useSpeed;
    public float directionSpeed = 10f;
    float origX;
    public float distance = 10f;

    void Start()
    {
       
        origX = transform.position.x;
        useSpeed = -directionSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(origX - transform.position.x > distance)
        {
            useSpeed = directionSpeed;
        }
        else if(origX - transform.position.x < -distance)
        {
            useSpeed = -directionSpeed;
        }
        transform.Translate(useSpeed * Time.deltaTime, 0, 0);
 
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
