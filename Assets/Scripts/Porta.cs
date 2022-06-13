using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour
{
    private float useSpeed;
    public float directionSpeed = 10f;
    float origY;
    public float distance = 10f;
    public bool acesso;
    public bool trigger;
    public AcendeSprite acende;
    PlayerMover PlayerMov;
    public Canvas texto;
    bool area;
    void Start()
    {
        area = false;
        texto.enabled = false;
        trigger = false;
        acesso = false;
        origY = transform.position.y;
        useSpeed = directionSpeed;
        PlayerMov = FindObjectOfType<PlayerMover>();
    }

    // Update is called once per frame
    void Update()
    {
        if (area)
        {
            texto.enabled = true;
        }
        else if (!area)
        {
            texto.enabled = false;
        }
        if (acesso)
        {
            acende.acendeu();
            if (origY - transform.position.y > distance)
            {
                useSpeed = 0;
            }
            else if (origY - transform.position.y < -distance)
            {
                useSpeed = 0;
            }
            transform.Translate(0, useSpeed * Time.deltaTime, 0);
        }
           
        if(trigger && Input.GetButtonDown("Acao"))
        {
            trigger = false;
            acesso = true;
            
        }
       
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && PlayerMov.chave && Input.GetButtonDown("Acao"))
        {     
            acesso = true;
            PlayerMov.chave = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            area = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            area = false;
        }
    }
}