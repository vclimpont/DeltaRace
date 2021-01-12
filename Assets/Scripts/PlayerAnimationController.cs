using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Rigidbody rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void RotateOnVelocityValue()
    {
        Vector3 v = rb.velocity;
        transform.rotation = Quaternion.Euler(-v.y, 0, -v.x);
    }

    public void StretchOnVelocityValue(Vector3 baseScale, float minSpeed, float maxSpeed)
    {
        Vector3 v = rb.velocity;
        float s = (v.z - minSpeed) / (3 * (maxSpeed - minSpeed));
        transform.localScale = baseScale + new Vector3(-s, 0, s);
    }

    public void PlayBoostAnimation(float animationTime)
    {
        Vector3 currentEulerRotation = transform.rotation.eulerAngles;
        bool right = currentEulerRotation.z > 180;

        StopCoroutine(BoostAnimation(currentEulerRotation, animationTime, right));
        StartCoroutine(BoostAnimation(currentEulerRotation, animationTime, right));
    }

    IEnumerator BoostAnimation(Vector3 startEulerAngles, float animationTime, bool right = true)
    {
        if(animationTime == 0)
        {
            yield return null;
        }

        float dt = 0;
        while(dt <= animationTime)
        {
            float side = right ? -1 : 1;
            float dtZ = Mathf.Lerp(0, side * 360f, dt / animationTime);
            transform.rotation = Quaternion.Euler(-rb.velocity.y, startEulerAngles.y, dtZ);
            dt += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }
}
