using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalableObjectBuilder : MonoBehaviour
{
    [SerializeField] private float minSize = 0f;
    [SerializeField] private float maxSize = 0f;
    [SerializeField] [Range(0.1f, 1f)] private float minScaleAmplitude = 0.1f;
    [SerializeField] [Range(0.1f, 1f)] private float maxScaleAmplitude = 0.1f;
    [SerializeField] [Range(0f, 1f)] private float scaleRatio = 0f;

    public void SetScalableObject(ScalableObjectComponent so)
    {
        float size = Random.Range(minSize, maxSize);
        float scaleAmplitude = Random.Range(minScaleAmplitude, maxScaleAmplitude);
        float scaleOverTime = Random.Range(0f, 1f);

        so.transform.localScale = Vector3.one * size;
        so.size = size;
        so.amplitude = scaleAmplitude;
        so.scaleOverTime = scaleOverTime <= scaleRatio;
    }
}
