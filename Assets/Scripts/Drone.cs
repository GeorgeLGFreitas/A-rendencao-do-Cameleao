using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public int damage;
    //public float timer = 10f;
    public Animator animator;
    public GameObject particula;
    AudioSource d_Audio;
   
    

    private void Start()
    {
        d_Audio = GetComponent<AudioSource>();
        
    }
    private void Update()
    {   
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMover p = collision.GetComponent<PlayerMover>();
        if (collision.tag == "Player" && !PlayerMover.invencivel && !PlayerMover.invDash )
        {
            p.TakeDamage(damage);
            Destroy(gameObject);
            GameObject e = Instantiate(particula) as GameObject;
            e.transform.position = transform.position;

        }
        else if (collision.tag == "Player" && PlayerMover.invDash)
        {
            Destroy(gameObject);
            GameObject e = Instantiate(particula) as GameObject;
            e.transform.position = transform.position;
        }
    }
    
  
}
