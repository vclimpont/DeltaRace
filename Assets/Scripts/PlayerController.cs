using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float minSpeed = 0;
    [SerializeField] private float maxSpeed = 0;
    [SerializeField] private float releaseSpeedFactor = 0;
    [SerializeField] private float diveSpeedFactor = 0;
    [SerializeField] private float turnSpeed = 0;
    [SerializeField] private float turnReleaseSpeedFactor = 0;
    [SerializeField] private float propulsionTimer = 0;

    private Rigidbody rb;
    private InputChecker ic;
    private PlayerAnimationController ac;

    private float minSpeedY;
    private float maxSpeedY;
    private Vector3 baseScale;

    private bool isPropelled;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ac = GetComponent<PlayerAnimationController>();
        ic = new InputChecker();

        minSpeedY = minSpeed;
        maxSpeedY = maxSpeed / 2;
        baseScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        ac.StretchOnVelocityValue(baseScale, minSpeed, maxSpeed);

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
        float z = v.z <= minSpeed ? minSpeed : v.z - releaseSpeedFactor;

        rb.velocity = new Vector3(x, y, z);
    }

    void Dive()
    {
        Vector3 v = rb.velocity;

        float x = ic.HorizontalMovement * turnSpeed;
        float y = v.y <= -maxSpeedY ? -maxSpeedY : v.y - diveSpeedFactor;
        float z = v.z >= maxSpeed ? maxSpeed : v.z + diveSpeedFactor;

        rb.velocity = new Vector3(x, y, z);
    }

    IEnumerator PropulsionTimeCounter()
    {
        isPropelled = true;
        yield return new WaitForSeconds(propulsionTimer);
        isPropelled = false;
    }

    public void StartPropelling()
    {
        ac.PlayBoostAnimation(propulsionTimer);
        isPropelled = true;
    }

    public void SetPropulsion()
    {
        StopCoroutine(PropulsionTimeCounter());
        StartCoroutine(PropulsionTimeCounter());
    }
}
