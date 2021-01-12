using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRB = null;

    void FixedUpdate()
    {
        transform.position += (playerRB.velocity * Time.deltaTime);
    }
}
