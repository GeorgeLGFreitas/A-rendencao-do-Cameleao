using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogTriggerTransição : MonoBehaviour
{

    public Dialog dialogo;
    public DialogManager2 dd;


    public void Start()
    {
        dd = GetComponent<DialogManager2>();
        dd.StartDialog(dialogo);
    }

    private void Update()
    {
        if(Input.GetButton("Acao"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        }
    }
}
