using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LigaEscolha : MonoBehaviour
{
    public DialogManager dd;
    public Collider2D escolha1;
    public Collider2D escolha2;
    // Start is called before the first frame update
    void Start()
    {
        escolha1.enabled = false;
        escolha1.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(dd.numero == 10)
        {
            escolha1.enabled = true;
            escolha1.enabled = true;

        }
        
    }
}
