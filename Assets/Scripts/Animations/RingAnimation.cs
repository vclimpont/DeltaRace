using UnityEngine;

public class RingAnimation : MonoBehaviour
{
    [SerializeField] private ParticleSystem psBurst = null;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") || (collider.CompareTag("AI") && collider.GetComponent<HangGliderComponent>().PlayParticles))
        {
            psBurst.Play();
        }
    }
}
