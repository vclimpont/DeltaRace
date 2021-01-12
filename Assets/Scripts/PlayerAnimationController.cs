using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Transform meshTransform;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 v = rb.velocity;
        RotateOnVelocityValue(v);
        StretchOnVelocityValue(v);
    }

    void RotateOnVelocityValue(Vector3 v)
    {
        transform.rotation = Quaternion.Euler(-v.y * 2f, 0, -v.x * 2f);
    }

    void StretchOnVelocityValue(Vector3 v)
    {
        float s = (v.z - 10f) / (3 * (30f - 10f));
        meshTransform.localScale = Vector3.one + new Vector3(-s, 0, s);
    }
}
