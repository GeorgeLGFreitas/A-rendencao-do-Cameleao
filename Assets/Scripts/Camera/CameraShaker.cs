using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    Vector3 cameraInitialPosition;
    public float shakeMagnitude = 0.05f;
    public float shakeTime = 0.05f;
    public Camera mainCamera;

    public void ShakeIt()
    {
        cameraInitialPosition = mainCamera.transform.position;
        InvokeRepeating("StartCameraShaking", 0f, 0.005f);
        Invoke("StopCameraShaking", shakeTime);
    }

    void StartCameraShaking()
    {
        float cameraShakingoffsetX = Random.value * shakeMagnitude * 2 - shakeMagnitude;
        float cameraShakingoffsety = Random.value * shakeMagnitude * 2 - shakeMagnitude;
        Vector3 cameraIntermadiatePosition = mainCamera.transform.position;
        cameraIntermadiatePosition.x += cameraShakingoffsetX;
        cameraIntermadiatePosition.y += cameraShakingoffsety;
        mainCamera.transform.position = cameraIntermadiatePosition;
    }
    void StopCameraShaking()
    {
        CancelInvoke("StartCameraShaking");
        mainCamera.transform.position = cameraInitialPosition;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
