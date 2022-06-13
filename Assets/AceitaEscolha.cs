using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AceitaEscolha : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioE;
    public Canvas escolha;
    public Fadeout fade;

    public Animator anim;
    private float timer;
    private float timer2;

    public int cena;
    bool aceita;
    void Start()
    {
        escolha.enabled = false;
        timer = 1;
        timer2 = 2;
        aceita = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(aceita)
        {
            
            timer -= Time.deltaTime;
        }
        if(timer <=0)
        {
            fade.FadeIn();
            timer2 -= Time.deltaTime;
            if (timer2 <= 0)
            {
                SceneManager.LoadScene(cena, LoadSceneMode.Single);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            escolha.enabled = true;
            if (Input.GetButton("Acao"))
            {
                aceita = true;
                audioE.Play();
                anim.SetBool("Escolha", true);
            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            escolha.enabled = false;
        }
    }
}
