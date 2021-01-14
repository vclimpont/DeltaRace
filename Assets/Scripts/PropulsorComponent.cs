using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropulsorComponent : MonoBehaviour
{
    [SerializeField] private float propulsionForce = 300f;
    [SerializeField] private Vector3 direction;

    private Vector3 propulsionVector;

    // Start is called before the first frame update
    void Awake()
    {
        propulsionVector = transform.rotation * direction;
    }

    void PropelPlayer(Vector3 playerPosition, Rigidbody rb)
    {
        rb.AddForce(propulsionForce * propulsionVector, ForceMode.Force);
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
        if (collider.CompareTag("Player"))
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
