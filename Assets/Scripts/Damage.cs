using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int valor;
    AudioSource d_audio;
    

    private void Start()
    {
        d_audio = GetComponent<AudioSource>();
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.CompareTag("Player"))
        {
            PlayerMover p = collision.GetComponent<PlayerMover>();
            p.TakeDamage(valor);
            Destroy(gameObject);



        }
    }
    
}
