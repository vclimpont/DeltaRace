using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CameraController cameraController = null;

    private InputChecker ic;
    private HangGliderComponent hgc; 

    private bool isPropelled;

    void Awake()
    {
        hgc = GetComponent<HangGliderComponent>();
        ic = new InputChecker();
    }

    // Update is called once per frame
    void Update()
    {
        if(isPropelled)
        {
            return;
        }

        ic.CheckDive();
        ic.CheckHorizontalMovement(transform.position);
    }

    void FixedUpdate()
    {
        if(isPropelled)
        {
            return;
        }

        hgc.Turn(ic.HorizontalMovement);

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
