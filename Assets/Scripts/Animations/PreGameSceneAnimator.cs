using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreGameSceneAnimator : MonoBehaviour
{
    [SerializeField] private Transform playerTransform = null;
    [SerializeField] private Transform cameraTransform = null;

    private Vector3 playerStartPosition;
    private Vector3 cameraStartPosition;

    void Awake()
    {
        playerStartPosition = playerTransform.position;
        cameraStartPosition = cameraTransform.position;
    }

    void MoveBackToStart()
    {
        playerTransform.position = playerStartPosition;
        cameraTransform.position = cameraStartPosition;
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            MoveBackToStart();
        }
    }
}
