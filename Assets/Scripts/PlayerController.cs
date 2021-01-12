using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
    private InputChecker ic;
    private PlayerAnimationController ac;

    private Vector3 baseScale;

    private bool isPropelled;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ac = GetComponent<PlayerAnimationController>();
        ic = new InputChecker();

        baseScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        ac.StretchOnVelocityValue(baseScale, minSpeedZ, maxSpeedZ);

        if(isPropelled)
        {
            return;
        }

        ic.CheckDive();
        ic.CheckHorizontalMovement(transform.position);

        ac.RotateOnVelocityValue();
    }

    void FixedUpdate()
    {
        if(isPropelled)
        {
            return;
        }

        if(!ic.IsDiving)
        {
            Release();
        }
        else
        {
            Dive();
        }
    }

    void Release()
    {
        Vector3 v = rb.velocity;

        float x = v.x - (turnReleaseSpeedFactor * v.x);
        float y = v.y >= minSpeedY ? minSpeedY : v.y + releaseSpeedFactor;
        float z = v.z <= minSpeedZ ? minSpeedZ : v.z - releaseSpeedFactor;

        rb.velocity = new Vector3(x, y, z);
    }

    void Dive()
    {
        Vector3 v = rb.velocity;

        float x = ic.HorizontalMovement * turnSpeed;
        float y = v.y <= -maxSpeedY ? -maxSpeedY : v.y - diveSpeedFactor;
        float z = v.z >= maxSpeedZ ? maxSpeedZ : v.z + diveSpeedFactor;

        rb.velocity = new Vector3(x, y, z);
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
            dt += Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }

        isPropelled = false;
        Debug.Log("release");
        yield return null;
    }

    public void StartPropelling()
    {
        Vector3 v = rb.velocity;
        rb.velocity = new Vector3(v.x, v.y, v.z / 2f);

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
