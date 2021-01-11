using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float releaseSpeedFactor;
    [SerializeField] private float diveSpeedFactor;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float turnReleaseSpeedFactor;

    private Rigidbody rb;
    private InputChecker ic;

    private float minSpeedY;
    private float maxSpeedY;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ic = new InputChecker();

        minSpeedY = minSpeed / 2;
        maxSpeedY = maxSpeed / 2;
    }

    // Update is called once per frame
    void Update()
    {
        ic.CheckInputs();
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

        //float x = Mathf.Clamp(v.x + turnReleaseSpeedFactor, 0, turnSpeed);
        float y = v.y >= minSpeedY ? minSpeedY : v.y + releaseSpeedFactor;
        float z = v.z <= minSpeed ? minSpeed : v.z - releaseSpeedFactor;

        rb.velocity = new Vector3(0, y, z);
    }

    void Dive()
    {
        Vector3 v = rb.velocity;

        //float x = Mathf.Clamp(v.x + ic.HorizontalMovement * turnSpeed, 0, turnSpeed);
        float y = v.y <= -maxSpeedY ? -maxSpeedY : v.y - diveSpeedFactor;
        float z = v.z >= maxSpeed ? maxSpeed : v.z + diveSpeedFactor;

        rb.velocity = new Vector3(0, y, z);
    }
}
