using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingAnimation : MonoBehaviour
{
    [SerializeField] private ParticleSystem psBurst;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") || collider.CompareTag("AI"))
        {
            psBurst.Play();
        }
    }
}
