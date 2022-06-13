using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform laserPoint;
    public GameObject laserPrefab;
    public float nextLaserTime = 0f;
    public float laserRate;
    private void Update()
    {
        if(Time.time >= nextLaserTime)
        {
            Instantiate(laserPrefab, laserPoint.position, laserPoint.rotation);
            nextLaserTime = Time.time + 1f / laserRate;
        }
    }
}
