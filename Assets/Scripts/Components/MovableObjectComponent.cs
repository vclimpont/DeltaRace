using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObjectComponent : MonoBehaviour
{
    public float speed { get; set; }
    public Vector2 moveDirection { get; set; }
    public float amplitude { get; set; }

    public bool spherical { get; set; }

    private Vector3 minPos;
    private Vector3 maxPos;
    private Vector3 basePosition;

    void Start()
    {
        minPos = transform.position - (Vector3)(amplitude * moveDirection);
        maxPos = transform.position + (Vector3)(amplitude * moveDirection);
        basePosition = transform.position;

        if (!spherical && speed > 0 && amplitude > 0)
        {
            StartCoroutine(TranslationMovement());
        }
    }

    void FixedUpdate()
    {
        if (speed == 0 || amplitude == 0)
        {
            return;
        }

        if (spherical)
        {
            SphericalMovement();
        }
    }

    void SphericalMovement()
    {
        float x = basePosition.x + Mathf.Cos(Time.time * speed) * amplitude;
        float y = basePosition.y + Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(x, y, basePosition.z);
    }

    IEnumerator TranslationMovement()
    {
        float dt = 0;
        Vector3 a = minPos;
        Vector3 b = maxPos;

        while (true)
        {
            transform.position = Vector3.Slerp(a, b, dt);
            dt += Time.deltaTime * speed;

            if (dt >= 1)
            {
                dt = 0;
                Vector3 c = a;
                a = b;
                b = c;
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
