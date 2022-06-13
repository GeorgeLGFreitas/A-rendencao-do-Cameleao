using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPlatform : MonoBehaviour
{
    private float useSpeed;
    public float directionSpeed = 10f;
    float origX;
    public float distanceX;
    bool move;

    void Start()
    {
        move = false;
        origX = transform.position.x;
        useSpeed = -directionSpeed;
    }
    private void Update()
    {
        origX = transform.position.x;
        if (origX >= distanceX)
        {
            useSpeed = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (move)
        {
           
            transform.Translate(useSpeed * Time.deltaTime, 0, 0);
        }
        

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(transform);
            move = true;

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
