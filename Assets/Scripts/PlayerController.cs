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

    private Rigidbody rb;
    private InputChecker ic;

    private float minSpeedY;
    private float maxSpeedY;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ic = new InputChecker();

        minSpeedY = minSpeed;
        maxSpeedY = maxSpeed / 2;
    }

    // Update is called once per frame
    void Update()
    {
        ic.CheckDive();
        ic.CheckHorizontalMovement(transform.position);
    }

    void FixedUpdate()
    {
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
}
