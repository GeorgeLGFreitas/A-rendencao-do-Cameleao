using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private CheckpointController cc;

    void Start()
    {
        cc = GameObject.FindGameObjectWithTag("CheckPoint").GetComponent<CheckpointController>();

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cc.LastCheckPointPos = transform.position;
        }
    }
}
