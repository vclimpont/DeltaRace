using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CameraController cameraController = null;
    [SerializeField] private float speedThresholdToEmitWindParticles = 0;

    private InputChecker ic;
    private HangGliderComponent hgc;
    private Rigidbody rb;

    void Awake()
    {
        hgc = GetComponent<HangGliderComponent>();
        ic = new InputChecker();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hgc.HasEnded)
        {
            return;
        }

        cameraController.EmitSpeedParticles(rb.velocity.magnitude >= speedThresholdToEmitWindParticles);

        if(hgc.isPropelled)
        {
            return;
        }

        ic.CheckDive();
        ic.CheckHorizontalMovement(transform.position);
    }

    void FixedUpdate()
    {
        if (hgc.HasEnded)
        {
            hgc.PlayEndBehaviour();
            return;
        }

        if (hgc.isPropelled)
        {
            return;
        }

        hgc.ApplyHorizontalMovement(ic.HorizontalMovement);

        if(!ic.IsDiving)
        {
            hgc.Release();
        }
        else
        {
            hgc.Dive();
        }
    }

    public void ShakeCamera()
    {
        cameraController.Shake();
    }

    void OnCollisionEnter(Collision coll)
    {
        ShakeCamera();
    }
}
