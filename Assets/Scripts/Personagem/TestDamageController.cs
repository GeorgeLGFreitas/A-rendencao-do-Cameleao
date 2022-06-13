using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDamageController : MonoBehaviour
{
    [SerializeField] private float theDamage;
    [SerializeField] private TestHealthController healthController;

    private void OntriggerEnter2D (Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Damage();
        }
    }

    public void Damage()
    {
        healthController.playerHealth = healthController.playerHealth - theDamage;
        healthController.UpdateHealth();
        Destroy(gameObject);
    }
}
