using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestHealthController : MonoBehaviour
{
    public float playerHealth;
    [SerializeField] private Text healthText;

    public void UpdateHealth()
    {
        healthText.text = playerHealth.ToString("0");
    }
}
