using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chave : MonoBehaviour
{
  
    void Update()
    {
        //transform.Rotate(0, 360 * Time.deltaTime, 0);
        transform.Rotate(0, 0, 120 * Time.deltaTime);
    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            Destroy(gameObject);
    }
}
