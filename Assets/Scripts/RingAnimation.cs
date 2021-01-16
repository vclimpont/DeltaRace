using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingAnimation : MonoBehaviour
{
    [SerializeField] private ParticleSystem psBurst = null;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") || collider.CompareTag("AI"))
        {
            psBurst.Play();
        }
    }
}
