using UnityEngine;

public class FanAnimation : MonoBehaviour
{
    [SerializeField] ParticleSystem windParticles;

    void Start()
    {
        if(LevelManager.Instance.LowSettings)
        {
            windParticles.Stop();
        }
    }
}
