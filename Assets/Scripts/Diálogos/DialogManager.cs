using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text nameText;

    public DialogTrigger Dt;
    public Text dialogText;
    public Image retrato;
    public Animator animator;
    public AcendeSprite acenSprite;
    public bool mudanome;


    public int numero;
   
    private Queue<string> dialogo;
    
    // Start is called before the first frame update
    void Start()
    {
       Dt = GetComponent<DialogTrigger>();
       dialogo = new Queue<string>();
       retrato = GetComponent<Image>();
 
       
    }
    void Update()
    {
       
        
        passafala();
        numero = dialogo.Count;
        nameText.text = Dt.dialogo.nome[numero];

   

    }
    public void StartDialog (Dialog dialogos)
    {
        acenSprite.acendeu();
        
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
        if(dialogo.Count > 0)
        {
            acenSprite.acendeu();
        }
        string fala = dialogo.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(fala));
    }
   
    void passafala()
    {
        if(Input.GetButtonDown("Acao"))
        {
            MostrarNovaFala();
            
        }
    }
    
    IEnumerator TypeSentence (string fala)
    {
        dialogText.text = "";
        foreach (char letter in fala.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
    }
    public void EndDialog()
    {
        acenSprite.apagou();

        animator.SetBool("isOpen", false);
        
        
    }
    
    
}
