using UnityEngine;

public class RingAnimation : MonoBehaviour
{
    [SerializeField] private ParticleSystem psBurst = null;

    private bool playParticles;

    public void Start()
    {
        playParticles = !LevelManager.Instance.LowSettings;
    }

    void OnTriggerEnter(Collider collider)
    {
        if(!playParticles)
        {
            return;
        }

        if (collider.CompareTag("Player") || (collider.CompareTag("AI") && collider.GetComponent<HangGliderComponent>().PlayParticles))
        {
            psBurst.Play();
        }
    }
}
