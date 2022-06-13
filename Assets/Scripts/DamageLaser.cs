using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageLaser : MonoBehaviour
{
    public int valor;
    public float laserTime;
    AudioSource l_audio;
    public AudioClip feixe;
    

    private void Start()
    {
        l_audio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMover p = collision.GetComponent<PlayerMover>();

        if (collision.tag == "Player" && !PlayerMover.invencivel)
        {
            p.TakeDamage(valor);
        }
    }
    private void Update()
    {
        if(laserTime > 0)
        {
            l_audio.PlayOneShot(feixe);
        }
     
        Destroy(gameObject, laserTime);

    }
}
