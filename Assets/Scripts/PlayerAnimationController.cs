using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController
{
    public Quaternion RotateOnVelocityValue(Vector3 v)
    {
        return Quaternion.Euler(-v.y, 0, -v.x);
    }

    public Vector3 StretchOnVelocityValue(Vector3 baseScale, Vector3 v, float minSpeed, float maxSpeed)
    {
        float s = (v.z - minSpeed) / (3 * (maxSpeed - minSpeed));
        return baseScale + new Vector3(-s, 0, s);
    }
}
