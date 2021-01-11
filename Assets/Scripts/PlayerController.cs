using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float releaseMinSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float releaseSpeedFactor;
    [SerializeField] private float diveSpeedFactor;

    private Rigidbody rb;
    private InputChecker ic;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ic = new InputChecker();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ic.CheckInputs();

        Debug.Log(ic.IsDiving + " " + ic.HorizontalMovement);
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
            Turn(ic.HorizontalMovement);
        }
    }

    void Release()
    {

    }

    void Dive()
    {

    }

    void Turn(float turnValue)
    {

    }
}
