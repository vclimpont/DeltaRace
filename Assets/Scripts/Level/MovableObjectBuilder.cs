using UnityEngine;

public class MovableObjectBuilder : MonoBehaviour
{
    [SerializeField] private float minSpeed = 0f;
    [SerializeField] private float maxSpeed = 0f;
    [SerializeField] private float minAmplitude = 0f;
    [SerializeField] private float maxAmplitude = 0f;
    [SerializeField][Range(0f, 1f)] private float sphericalRatio = 0f;
    [SerializeField][Range(0f, 1f)] private float maxLevelMultiplier = 0f;

    void Start()
    {
        maxSpeed *= (1f + Mathf.Clamp(LevelManager.Instance.CurrentLevel * 0.02f, 0f, maxLevelMultiplier));
        maxAmplitude *= (1f + Mathf.Clamp(LevelManager.Instance.CurrentLevel * 0.02f, 0f, maxLevelMultiplier));
    }

    public void SetMovableObject(MovableObjectComponent mo)
    {
        float speed = Random.Range(minSpeed, maxSpeed);
        float amplitude = Random.Range(minAmplitude, maxAmplitude);

        float dirX = Random.Range(-1f, 1f);
        float dirY = Random.Range(-1f, 1f);
        float spherical = Random.Range(0f, 1f);

        mo.speed = speed;
        mo.amplitude = amplitude;
        mo.moveDirection = new Vector2(dirX, dirY).normalized;
        mo.spherical = spherical <= sphericalRatio;
    }

}
