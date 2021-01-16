using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangGliderAnimationController : MonoBehaviour
{
    [SerializeField][Range(0.01f, 1f)] private float maxShineIntensity = 0.01f;
    [SerializeField] private float speedThresholdToEmitParticles = 0f;
    [SerializeField] private ParticleSystem[] psTrails = null;
    [SerializeField] private ParticleSystem psBoost = null;

    private Rigidbody rb;
    private MeshRenderer mesh;
    private Animator endBehaviourAnimator;
    private bool isBoostAnimationReady;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mesh = GetComponent<MeshRenderer>();
        endBehaviourAnimator = GetComponent<Animator>();
        isBoostAnimationReady = true;
    }

    public void RotateOnVelocityValue(bool rotateX = true)
    {
        Vector3 v = rb.velocity;
        float rY = Mathf.Clamp(-v.y * 2, -90f, 60f);
        float rX = rotateX ? Mathf.Clamp(-v.x * 2, -90f, 90f) : transform.rotation.eulerAngles.z;
        transform.rotation = Quaternion.Euler(rY, 0, rX);
    }

    public void StretchOnVelocityValue(Vector3 baseScale, float minSpeed, float maxSpeed)
    {
        Vector3 v = rb.velocity;
        float s = (v.z - minSpeed) / (maxSpeed - minSpeed);
        transform.localScale = baseScale + new Vector3(-s * 0.5f, 0, 0.3f * s);
    }

    public void ShineOnVelocityValue(float minSpeed, float maxSpeed)
    {
        float speed = rb.velocity.magnitude;
        float dtI = (speed - minSpeed) / ((maxSpeed - minSpeed) / maxShineIntensity);

        Color matColor = mesh.material.color;
        mesh.material.SetColor("_EmissionColor", matColor * dtI);
    }

    public void PlayBoostAnimation(bool withParticles = true)
    {
        if(!isBoostAnimationReady)
        {
            return;
        }

        if(withParticles)
        {
            psBoost.Play();
        }

        Vector3 currentEulerRotation = transform.rotation.eulerAngles;
        bool right = currentEulerRotation.z > 180;

        StopCoroutine(BoostAnimation(0.5f, right));
        StartCoroutine(BoostAnimation(0.5f, right));
    }

    public void EmitTrailsParticles()
    {
        if (psTrails == null || psTrails.Length != 2)
        {
            return;
        }

        float m = rb.velocity.magnitude;

        if (m >= speedThresholdToEmitParticles && !psTrails[0].isPlaying)
        {
            psTrails[0].Play();
            psTrails[1].Play();
        }
        else if (m < speedThresholdToEmitParticles && psTrails[0].isPlaying)
        {
            psTrails[0].Stop();
            psTrails[1].Stop();
        }
    }

    public void PlayEndBehaviourAnimation()
    {
        endBehaviourAnimator.enabled = true;
    }

    IEnumerator BoostAnimation(float animationTime, bool right = true)
    {
        if(animationTime == 0)
        {
            yield return null;
        }

        isBoostAnimationReady = false;

        float dt = 0;
        while(dt <= animationTime)
        {
            dt += Time.deltaTime;
            float side = right ? -1 : 1;
            float dtZ = Mathf.Lerp(0, side * 360f, dt / animationTime);
            transform.rotation = Quaternion.Euler(0, 0, dtZ);

            yield return new WaitForEndOfFrame();
        }

        transform.rotation = Quaternion.Euler(0, 0, 0);
        isBoostAnimationReady = true;

        yield return null;
    }
}
