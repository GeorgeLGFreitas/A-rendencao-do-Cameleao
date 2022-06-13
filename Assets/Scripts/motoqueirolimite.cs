using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class motoqueirolimite : MonoBehaviour
{
    public Rigidbody2D rb;
    public float limiteYup;
    public float limiteYdown;
    public float limiteXleft;
    public float limiteXright;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (rb.position.y < limiteYdown)
        {
            rb.position = new Vector2(rb.position.x, limiteYdown);
        }
        else if (rb.position.y > limiteYup)
        {
            rb.position = new Vector2(rb.position.x, limiteYup);
        }

        if (rb.position.x < limiteXleft)
        {
            rb.position = new Vector2(limiteXleft, rb.position.y);

        }
        else if (rb.position.x > limiteXright)
        {
            rb.position = new Vector2(limiteXright, rb.position.y);
        }
    }
}
