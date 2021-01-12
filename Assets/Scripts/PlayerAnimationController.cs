using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Rigidbody rb;
    private bool boostAnimationEnded;
    private float boostAnimationCooldown;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        boostAnimationEnded = true;
        boostAnimationCooldown = 0f;
    }

    public void RotateOnVelocityValue()
    {
        if(!boostAnimationEnded)
        {
            return;
        }

        Vector3 v = rb.velocity;
        transform.rotation = Quaternion.Euler(-v.y * 2, 0, -v.x * 2);
    }

    public void StretchOnVelocityValue(Vector3 baseScale, float minSpeed, float maxSpeed)
    {
        Vector3 v = rb.velocity;
        float s = (v.z - minSpeed) / (2 * (maxSpeed - minSpeed));
        transform.localScale = baseScale + new Vector3(-s, 0, s);
    }

    public void PlayBoostAnimation(float animationTime)
    {
        if(boostAnimationCooldown > 0)
        {
            return;
        }
        StopCoroutine(BoostAnimationCooldown());
        StartCoroutine(BoostAnimationCooldown());

        Vector3 currentEulerRotation = transform.rotation.eulerAngles;
        bool right = currentEulerRotation.z > 180;

        StopCoroutine(BoostAnimation(currentEulerRotation, 0.5f, right));
        StartCoroutine(BoostAnimation(currentEulerRotation, 0.5f, right));
    }

    IEnumerator BoostAnimation(Vector3 startEulerAngles, float animationTime, bool right = true)
    {
        if(animationTime == 0)
        {
            yield return null;
        }

        boostAnimationEnded = false;
        float dt = 0;
        while(dt <= animationTime)
        {
            float side = right ? -1 : 1;
            float dtZ = Mathf.Lerp(0, side * 360f, dt / animationTime);
            transform.rotation = Quaternion.Euler(0, 0, dtZ);
            dt += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        boostAnimationEnded = true;
        yield return null;
    }

    IEnumerator BoostAnimationCooldown()
    {
        boostAnimationCooldown = 0.2f;
        while(boostAnimationCooldown > 0)
        {
            boostAnimationCooldown -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        boostAnimationCooldown = 0;
        yield return null;
    }
}
