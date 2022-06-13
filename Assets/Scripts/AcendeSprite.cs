using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcendeSprite : MonoBehaviour
{
    public SpriteRenderer sprite;
    
    Color cor;
    bool acende;
    private float tempo;
    // Start is called before the first frame update
    void Start()
    {
        
        acende = false;
     
        cor = sprite.color;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        cor = sprite.color;
        if (acende)
        {
           
            cor.a += 0.1f;
            if (cor.a >= 1.0f)
            {
                cor.a = 1.0f;
            }


        }
        sprite.color = cor;
        

        if (!acende)
        {
            
            cor.a -= 0.1f;
            if(cor.a <= 0.0f)
            {
                cor.a = 0.0f;
            }
        }
        sprite.color = cor;

        

    }
    public void acendeu()
    {
        acende = true;
        
    }
    public void apagou()
    {
        acende = false;
    
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            acendeu();
        }
       
    }
    


}

