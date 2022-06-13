using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPos : MonoBehaviour
{
    private CheckpointController cc;
  

    private void Start()
    {
        cc = GameObject.FindGameObjectWithTag("CheckPoint").GetComponent<CheckpointController>();
        transform.position = cc.LastCheckPointPos;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
 
        }
    }
}
