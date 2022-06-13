using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager2 : MonoBehaviour
{
    public Text nameText;

    public DialogTriggerTransição Dt;
    public Text dialogText;
    public bool mudanome;

    public int numero;

    private Queue<string> dialogo;

    // Start is called before the first frame update
    void Start()
    {
        Dt = GetComponent<DialogTriggerTransição>();
        dialogo = new Queue<string>();
       


    }
    void Update()
    {


        passafala();
        numero = dialogo.Count;
        nameText.text = Dt.dialogo.nome[numero];



    }
    public void StartDialog(Dialog dialogos)
    {
        

        nameText.text = dialogos.nome[numero];

        dialogo.Clear();



        foreach (string fala in dialogos.dialogo)
        {
            dialogo.Enqueue(fala);

        }


        MostrarNovaFala();

    }

    public void MostrarNovaFala()
    {
        if (dialogo.Count == 0)
        {

            this.EndDialog();

            return;
        }

        string fala = dialogo.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(fala));
    }

    void passafala()
    {
        if (Input.GetButtonDown("Acao"))
        {
            MostrarNovaFala();

        }
    }

    IEnumerator TypeSentence(string fala)
    {
        dialogText.text = "";
        foreach (char letter in fala.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
    }
    private void EndDialog()
    {


      

    }


}
