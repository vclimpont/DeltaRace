using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform = null;
    [SerializeField] private Animator animator = null;
    [SerializeField] private float stiffnessFactor = 0.01f;
    [SerializeField] private float delayBetweenShakes = 0.01f;

    private Vector3 cameraDistanceFromPlayer;
    private bool canShake;
    private ParticleSystem psCamera;
    private bool playParticles;

    void Awake()
    {
        psCamera = GetComponentInChildren<ParticleSystem>();
    }

    void Start()
    {
        cameraDistanceFromPlayer = transform.position - playerTransform.position;
        canShake = true;

        if(LevelManager.Instance != null)
        {
            playParticles = !LevelManager.Instance.LowSettings;
        }
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = playerTransform.position + cameraDistanceFromPlayer;
        transform.position = Vector3.Slerp(transform.position, targetPosition, stiffnessFactor);
    }

    public void Shake()
    {
        if (!canShake)
        {
            return;
        }

        animator.SetTrigger("Shake");

        StopCoroutine(ShakeCooldown());
        StartCoroutine(ShakeCooldown());
    }

    public void EmitSpeedParticles(bool emit)
    {
        if(!playParticles)
        {
            return;
        }

        if (emit && !psCamera.isPlaying)
        {
            psCamera.Play();
        }
        else if (!emit && psCamera.isPlaying)
        {
            psCamera.Stop();
        }
    }

    IEnumerator ShakeCooldown()
    {
        canShake = false;
        yield return new WaitForSeconds(delayBetweenShakes);
        canShake = true;
    }
}
