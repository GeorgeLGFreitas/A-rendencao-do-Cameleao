using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DelayTransiçao : MonoBehaviour
{
    public float delay;
    private float limit;
    public int number;
    public GameObject Canvas;
    // Start is called before the first frame update
    void Start()
    {
        limit = 0;
        

    }

    // Update is called once per frame
    void Update()
    {
     
        
        if (Canvas.gameObject.activeSelf)
        {

            limit += Time.deltaTime;
   

        }

        if(limit>=delay)
        {
            SceneManager.LoadScene(number);

            
        }
    }
}
