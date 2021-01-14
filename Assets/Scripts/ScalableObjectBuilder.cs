using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalableObjectBuilder : MonoBehaviour
{
    [SerializeField] private float minSize;
    [SerializeField] private float maxSize;
    [SerializeField] [Range(0.1f, 1f)] private float minScaleAmplitude;
    [SerializeField] [Range(0.1f, 1f)] private float maxScaleAmplitude;
    [SerializeField] [Range(0f, 1f)] private float scaleRatio;

    public void SetScalableObject(ScalableObjectComponent so)
    {
        float size = Random.Range(minSize, maxSize);
        float scaleAmplitude = Random.Range(minScaleAmplitude, maxScaleAmplitude);
        float scaleOverTime = Random.Range(0f, 1f);

        so.transform.localScale = new Vector3(size, size, 0.5f);
        so.size = size;
        so.amplitude = scaleAmplitude;
        so.scaleOverTime = scaleOverTime <= scaleRatio;
    }
}
