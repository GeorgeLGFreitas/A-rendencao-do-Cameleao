using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivactionArea : MonoBehaviour
{
    public AIDestinationSetter AI;
    public Drone drone;
    bool trigger = false;
    private void Start()
    {
        drone.enabled = false;
        AI.enabled = false;
    }
    void Update()
    {
        if(trigger == true)
        {
            drone.enabled = true;
            AI.enabled = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            trigger = true;
        }
        
    }
}
