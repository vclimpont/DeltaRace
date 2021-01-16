using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostGameSceneAnimator : MonoBehaviour
{
    [SerializeField] private Transform endTransform;

    void OnTriggerEnter(Collider collider)
    {
        HangGliderComponent hgc = collider.GetComponent<HangGliderComponent>();

        if (hgc != null)
        {
            Vector3 endPosition = endTransform.position + new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));

            hgc.HasEnded = true;
            hgc.EndPosition = endPosition;
        }
    }
}
