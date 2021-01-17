using UnityEngine;

public class PropulsorComponent : MonoBehaviour
{
    [SerializeField] private float propulsionForce = 300f;
    [SerializeField] private Vector3 direction = Vector3.zero;

    private Vector3 propulsionVector;

    // Start is called before the first frame update
    void Awake()
    {
        propulsionVector = transform.rotation * direction;
    }

    void PropelPlayer(Rigidbody rb)
    {
        rb.AddForce(propulsionForce * propulsionVector, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider collider)
    {
        HangGliderComponent hgc = collider.GetComponent<HangGliderComponent>();

        if (collider.CompareTag("Player"))
        {
            AudioManager.Instance.Play("Fan");
            collider.GetComponent<PlayerController>().ShakeCamera();
        }

        if(hgc != null)
        {
            hgc.StartPropelling();
        }

    }

    private void OnTriggerStay(Collider collider)
    {
        HangGliderComponent hgc = collider.GetComponent<HangGliderComponent>();
        if (hgc != null)
        {
            hgc.SetPropulsion();
            PropelPlayer(collider.attachedRigidbody);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        HangGliderComponent hgc = collider.GetComponent<HangGliderComponent>();
        if (hgc != null)
        {
            hgc.PropulsionStun();
        }
    }
}
