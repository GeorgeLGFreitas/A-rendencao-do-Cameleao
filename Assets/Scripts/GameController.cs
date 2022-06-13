using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static int vidas=3;
    public static float score=0;
    public GameObject player;
    public GameObject gameover;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(vidas == 0)
        {
            Destroy(player.gameObject);
            gameover.SetActive(true);      
        }
        
    }
}
