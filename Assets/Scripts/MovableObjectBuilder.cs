using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObjectBuilder : MonoBehaviour
{
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minAmplitude;
    [SerializeField] private float maxAmplitude;
    [SerializeField][Range(0f, 1f)] private float sphericalRatio;

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
