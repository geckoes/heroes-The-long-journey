using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyFixingCamera : MonoBehaviour
{
    protected float cameraAngle;

    private void Start()
    {
        Camera cam = Camera.main;
        cameraAngle = cam.transform.eulerAngles.x;
        RotateTowardsCamera();
    }

    protected void RotateTowardsCamera()
    {
        transform.rotation = Quaternion.Euler(cameraAngle, 0f, 0f);
    }
}
