using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AperteV : MonoBehaviour
{
    public Canvas texto;
     bool area;
    // Start is called before the first frame update
    void Start()
    {
        area = false;
        texto.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (area)
        {
            texto.enabled = true;
        }
        else if(!area)
        {
            texto.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            area = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            area = false;
        }
    }
}
