using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiBodyRotate2DScript : BodyFixingCamera
{
   void Update()
    {
        RotateTowardsCamera();
    }
}
