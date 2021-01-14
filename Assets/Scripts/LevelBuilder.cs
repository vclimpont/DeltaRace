using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private int layersSize = 0;
    [SerializeField] private int floorsSize = 0;
    [SerializeField] private float offsetBetweenLayers = 0f;
    [SerializeField] private float offsetBetweenFloors = 0f;
    [SerializeField] private float startY = 0f;
    [SerializeField] private float startZ = 0f;

    [SerializeField][Range(0f, 1f)] private float obstaclesRatio = 0f;
    [SerializeField][Range(0f, 1f)] private float fansRatio = 0f;
    [SerializeField] private float deltaRandomRotation;
    [SerializeField] private float deltaRandomScale;

    [SerializeField] private GameObject ringPrefab = null;

    private float[] xPositions;
    private RingBuilder rbuild;

    // Start is called before the first frame update
    void Start()
    {
        xPositions = new float[] { -2 };
        rbuild = GetComponent<RingBuilder>();

        BuildLevel();
    }

    private void BuildRing(Vector3 position)
    {
        GameObject ring = Instantiate(ringPrefab, position, Quaternion.identity);
        rbuild.SetRing(ring.GetComponent<RingComponent>());
    }

    private void BuildFloor(float y, float z)
    {
        for (int i = 0; i < xPositions.Length; i++)
        {
            float r = Random.Range(0f, 1f);

            if(r <= obstaclesRatio)
            {
                BuildRing(new Vector3(xPositions[i], y, z));
            }
        }
    }

    private void BuildLayer(float z)
    {
        float fansRatioBuffer = fansRatio;

        float y = startY;
        for (int i = 0; i < floorsSize; i++)
        {
            BuildFloor(y, z);
            y += offsetBetweenFloors;
            fansRatio -= 0.25f;
        }

        fansRatio = fansRatioBuffer;
    }

    public void BuildLevel()
    {
        float z = startZ;
        for (int i = 0; i < layersSize; i++)
        {
            BuildLayer(z);
            z += offsetBetweenLayers;
        }
    }

}
