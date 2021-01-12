using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanController : MonoBehaviour
{
    [SerializeField] private Transform fanMeshTransform = null;
    [SerializeField] private Transform anchorPoint = null;
    [SerializeField] private float maxWindForce = 300f;

    private Vector3 windDirection;

    // Start is called before the first frame update
    void Awake()
    {
        windDirection = fanMeshTransform.rotation * new Vector3(0, 1, 0);
    }

    void PropelPlayer(Vector3 playerPosition, Rigidbody rb)
    {
        //float dist = Mathf.Abs(playerPosition.y - anchorPoint.position.y);
        //Debug.Log(dist);

        //float dtForce = maxWindForce * (10f - dist);
        rb.AddForce(maxWindForce * windDirection, ForceMode.Force);
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
}
