using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaCai : MonoBehaviour
{
    Rigidbody2D plataforma;
    bool ativa;
    public float plataTime;
    
    // Start is called before the first frame update
    void Start()
    {
        plataforma = GetComponent<Rigidbody2D>();
        ativa = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ativa)
        {
            plataTime -= Time.deltaTime; 
            
        }
        if ( plataTime <= 0)
        {
            plataforma.constraints = ~RigidbodyConstraints2D.FreezePositionY; ;
            plataforma.AddForce(new Vector2(0, -1));

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            ativa = true;
        }
        if(collision.CompareTag("TileMap"))
        {
            Destroy(gameObject);
        }
    }
   
}
