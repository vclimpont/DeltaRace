using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform = null;
    [SerializeField] private float stiffnessFactor = 0.01f;

    private Vector3 cameraDistanceFromPlayer;

    void Start()
    {
        cameraDistanceFromPlayer = transform.position - playerTransform.position;
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = playerTransform.position + cameraDistanceFromPlayer;
        transform.position = Vector3.Slerp(transform.position, targetPosition, stiffnessFactor);
    }
}
