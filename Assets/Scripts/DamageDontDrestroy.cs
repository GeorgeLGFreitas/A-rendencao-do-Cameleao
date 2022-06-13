using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDontDrestroy : MonoBehaviour
{
    public int valor;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            FindObjectOfType<PlayerMover>().TakeDamage(1);
        }
    }
}
