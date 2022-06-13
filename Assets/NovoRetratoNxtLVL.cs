using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NovoRetratoNxtLVL : MonoBehaviour
{
    Image mImage;
    public DialogManagerNxtLVL retrato1;
    public Sprite[] expres;
    int numero;

    // Start is called before the first frame update
    void Start()
    {
        mImage = this.GetComponent<Image>();

        numero = 0;
    }

    // Update is called once per frame
    void Update()
    {
        numero = retrato1.numero;
        mImage.sprite = expres[numero];
    }
}
