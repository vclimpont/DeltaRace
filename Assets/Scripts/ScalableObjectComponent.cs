using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalableObjectComponent : MonoBehaviour
{
    public float size { get; set; }
    public float amplitude { get; set; }
    public bool scaleOverTime { get; set; }

    private Vector3 baseScale;
    private ParticleSystem[] ps;

    void Start()
    {
        baseScale = transform.localScale;

        ps = GetComponentsInChildren<ParticleSystem>();
        ScaleParticles(baseScale);
    }

    void FixedUpdate()
    {
        if (scaleOverTime && amplitude > 0)
        {
            Rescale();
        }
    }

    void Rescale()
    {
        float s = Mathf.PingPong(Time.time, 2 * amplitude);
        s -= amplitude;
        Vector3 dtScale = new Vector3(baseScale.x * s, baseScale.y * s, 0);

        transform.localScale = baseScale + dtScale;
        ScaleParticles(baseScale + dtScale);
    }

    void ScaleParticles(Vector3 scale)
    {
        for (int i = 0; i < ps.Length; i++)
        {
            ps[i].transform.localScale = scale;
        }
    }
}
