using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            //transform.Rotate(0, 360 * Time.deltaTime, 0);
            transform.Rotate(0, 0, 120 * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindObjectOfType<PlayerMover>().Heal();
            FindObjectOfType<PlayerMover>().HealthUpdate();
            Destroy(gameObject);
        }
    }
}
