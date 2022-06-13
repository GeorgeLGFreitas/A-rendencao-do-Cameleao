using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cheats : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject teleporte;
    bool invencivel2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("f1"))
        {
            transform.position = new Vector2(teleporte.transform.position.x, teleporte.transform.position.y);
        }
        if(Input.GetKeyDown("f3"))
        {
            invencivel2 = true;
            
        }
        if (Input.GetKeyDown("f2"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1, LoadSceneMode.Single);
            Destroy(gameObject);
            Destroy(FindObjectOfType<CheckpointController>());
        }
        if (invencivel2)
        {
            PlayerMover.invencivel = true;
            FindObjectOfType<PlayerMover>().afterDamageInvulnerable += 999999;
        }
    }
}
