using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrulha : MonoBehaviour
{
    public float vely;
    public float limite_up;
    public float limite_down;

    public float velx;
    public float limite_esq;
    public float limite_dir;
   

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(velx > 0 && transform.position.x >= limite_dir)
        {
            velx = 0;
            //transform.localScale = new Vector3(-1,1,1);
        }
        else if(velx < 0 && transform.position.x <= limite_esq)
        {
            velx = -velx;
            //transform.localScale = new Vector3(1, 1, 1);
        }
        
        transform.Translate(velx * Time.deltaTime, 0, 0);

        if (vely > 0 && transform.position.y >= limite_up)
        {
            vely = -vely;
        }
        else if (vely < 0 && transform.position.y <= limite_down)
        {
            vely = -vely;            
        }

        transform.Translate(0, vely * Time.deltaTime, 0);
    }
}
