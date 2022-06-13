using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogTriggerGene : MonoBehaviour
{

    public Dialog dialogo;
    public DialogManagerNxtLVL dd;
    public Animator animator;
   

    public void Start()
    {
        dd = GetComponent<DialogManagerNxtLVL>();
    }

    private void Update()
    {
       
    }

    public void TriggerDialog()
    {

        dd.StartDialog(dialogo);
        animator.SetBool("isOpen", true);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TriggerDialog();


        }
    }
}
