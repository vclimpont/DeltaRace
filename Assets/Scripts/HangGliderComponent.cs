using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangGliderComponent : MonoBehaviour
{
    [Header("Physics Settings")]
    [SerializeField] private float minSpeedZ = 0;
    [SerializeField] private float maxSpeedZ = 0;
    [SerializeField] private float minSpeedY = 0;
    [SerializeField] private float maxSpeedY = 0;
    [SerializeField] private float releaseSpeedFactor = 0;
    [SerializeField] private float diveSpeedFactor = 0;
    [SerializeField] private float turnSpeed = 0;
    [SerializeField] private float turnReleaseSpeedFactor = 0;
    [SerializeField] private float propulsionTimer = 0;
    [SerializeField] private float propulsionReleaseTimer = 0;

    private Rigidbody rb;
    public HangGliderAnimationController ac { get; private set; }

    private Vector3 baseScale;
    private ParticleSystem psTrail;

    public bool isPropelled { get; set; }
    public bool rotateX { get; set; }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ac = GetComponent<HangGliderAnimationController>();

        baseScale = transform.localScale;
        rotateX = true;
    }

    // Update is called once per frame
    void Update()
    {
        ac.StretchOnVelocityValue(baseScale, minSpeedZ, maxSpeedZ);
        ac.RotateOnVelocityValue(!isPropelled && rotateX);
        ac.ShineOnVelocityValue(minSpeedZ, maxSpeedZ);
        ac.EmitTrailsParticles();
    }

    public void Release()
    {
        Vector3 v = rb.velocity;

        float x = v.x - (turnReleaseSpeedFactor * v.x);
        float y = v.y >= minSpeedY ? minSpeedY : v.y + releaseSpeedFactor;
        float z = v.z <= minSpeedZ ? minSpeedZ : v.z - releaseSpeedFactor;

        rb.velocity = new Vector3(x, y, z);
    }

    public void Dive()
    {
        Vector3 v = rb.velocity;

        float y = v.y <= -maxSpeedY ? -maxSpeedY : v.y - diveSpeedFactor;
        float z = v.z >= maxSpeedZ ? maxSpeedZ : v.z + diveSpeedFactor;

        rb.velocity = new Vector3(v.x, y, z);
    }

    public void Turn(float horizontalMovement)
    {
        Vector3 v = rb.velocity;

        float x = horizontalMovement * turnSpeed;

        rb.velocity = new Vector3(x, v.y, v.z);
    }

    public void MoveTowards(Vector3 position)
    {
        Vector3 targetDirection = (position - transform.position).normalized;
        float speed = position.y < transform.position.y ? maxSpeedZ : minSpeedZ;
        rb.velocity = targetDirection * speed;
    }

    IEnumerator PropulsionTimeCounter()
    {
        yield return new WaitForSeconds(propulsionTimer);

        float endPropulsionVelocityY = rb.velocity.y;
        StopCoroutine(PropulsionRelease(endPropulsionVelocityY));
        StartCoroutine(PropulsionRelease(endPropulsionVelocityY));
    }

    IEnumerator PropulsionRelease(float endPropulsionVelocityY)
    {
        float dt = 0;
        while (dt <= propulsionReleaseTimer)
        {
            float dtY = Mathf.Lerp(endPropulsionVelocityY, minSpeedY, dt / propulsionReleaseTimer);

            Vector3 v = rb.velocity;
            rb.velocity = new Vector3(v.x, dtY, v.z);
            dt += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        isPropelled = false;
        yield return null;
    }

    public void StartPropelling()
    {
        Vector3 v = rb.velocity;
        rb.velocity = new Vector3(v.x, v.y, v.z / 1.5f);

        ac.PlayBoostAnimation(propulsionTimer);
        isPropelled = true;
    }

    public void SetPropulsion()
    {
        StopAllCoroutines();
        isPropelled = true;
    }

    public void PropulsionStun()
    {
        StopCoroutine(PropulsionTimeCounter());
        StartCoroutine(PropulsionTimeCounter());
    }
}
