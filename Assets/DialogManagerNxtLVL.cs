using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class DialogManagerNxtLVL : MonoBehaviour
{
    public Text nameText;
    public NextLevel nxtLVL;
    public DialogTriggerGene Dt;
    public Text dialogText;
    public Image retrato;
    public Animator animator;
    public AcendeSprite acenSprite;
    public bool mudanome;


    public int numero;
    public bool nextLVL;
    bool check;
    private Queue<string> dialogo;

    // Start is called before the first frame update
    void Start()
    {
        Dt = GetComponent<DialogTriggerGene>();
        dialogo = new Queue<string>();
        retrato = GetComponent<Image>();
        nextLVL = false;
        check = false;
    }
    void Update()
    {


        passafala();
        numero = dialogo.Count;
        nameText.text = Dt.dialogo.nome[numero];

        if (nextLVL)
        {
            nxtLVL.carrega = true;
        }

    }
    public void StartDialog(Dialog dialogos)
    {
        acenSprite.acendeu();
        
        nameText.text = dialogos.nome[numero];

        dialogo.Clear();
        


        foreach (string fala in dialogos.dialogo)
        {
            dialogo.Enqueue(fala);

        }

        
        MostrarNovaFala();
        check = true;
        
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
        if (dialogo.Count > 0)
        {
            acenSprite.acendeu();
        }
        if (Input.GetButtonDown("Acao") && check)
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
    void EndDialog()
    {
        acenSprite.apagou();
        nextLVL = true;
        animator.SetBool("isOpen", false);
       
        

    }
  

}
