using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingBuilder : MonoBehaviour
{
    [SerializeField] private float minSize;
    [SerializeField] private float maxSize;
    [SerializeField] private int[] scores;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minAmplitude;
    [SerializeField] private float maxAmplitude;
    [SerializeField][Range(0f, 1f)] private float sphericalRatio;

    public void SetRing(RingComponent ring)
    {
        float scale = Random.Range(minSize, maxSize);
        float speed = Random.Range(minSpeed, maxSpeed);
        float amplitude = Random.Range(minAmplitude, maxAmplitude);
        int scoreIndex = Random.Range(0, scores.Length);

        float dirX = Random.Range(-1f, 1f);
        float dirY = Random.Range(-1f, 1f);
        float spherical = Random.Range(0f, 1f);

        ring.transform.localScale = new Vector3(scale, scale, 0.5f);
        ring.size = scale;
        ring.speed = speed;
        ring.score = scores[scoreIndex];
        ring.amplitude = amplitude;
        ring.moveDirection = new Vector2(dirX, dirY).normalized;
        ring.spherical = spherical <= sphericalRatio;
    }

}
