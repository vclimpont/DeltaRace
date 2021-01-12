using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanController : MonoBehaviour
{
    [SerializeField] private Transform fanMeshTransform = null;
    [SerializeField] private float maxWindForce = 300f;

    private Vector3 windDirection;

    // Start is called before the first frame update
    void Awake()
    {
        windDirection = fanMeshTransform.rotation * new Vector3(0, 1, 0);
    }

    void PropelPlayer(Vector3 playerPosition, Rigidbody rb)
    {
        rb.AddForce(maxWindForce * windDirection, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerController playerController = collider.GetComponent<PlayerController>();
            playerController.StartPropelling();
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            PlayerController playerController = collider.GetComponent<PlayerController>();
            playerController.SetPropulsion();
            PropelPlayer(collider.transform.position, collider.attachedRigidbody);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerController playerController = collider.GetComponent<PlayerController>();
            playerController.PropulsionStun();
        }
    }
}
