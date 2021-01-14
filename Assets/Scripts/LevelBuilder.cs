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

    [SerializeField] private GameObject obstaclePrefab = null;

    private float[] xPositions;

    // Start is called before the first frame update
    void Start()
    {
        xPositions = new float[] { -12f, -2, 8f };

        BuildLevel();
    }

    private Vector3 GetObstacleRandomScale()
    {
        float rX = Random.Range(-deltaRandomScale, deltaRandomScale);
        float rY = Random.Range(-deltaRandomScale, deltaRandomScale);
        float rZ = Random.Range(-deltaRandomScale, deltaRandomScale);

        return new Vector3(rX, rY, rZ);
    }

    private Vector3 GetObstacleRandomRotation()
    {
        float rX = Random.Range(-deltaRandomRotation, deltaRandomRotation);
        float rY = Random.Range(-deltaRandomRotation, deltaRandomRotation);
        float rZ = Random.Range(-deltaRandomRotation, deltaRandomRotation);

        return new Vector3(rX, rY, rZ);
    }

    private void BuildObstacle(Vector3 position)
    {
        GameObject obstacle = Instantiate(obstaclePrefab, position, Quaternion.Euler(GetObstacleRandomRotation()));
        obstacle.transform.GetChild(0).transform.localScale += GetObstacleRandomScale();

        float r = Random.Range(0f, 1f);

        if(r > fansRatio)
        {
            obstacle.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    private void BuildFloor(float y, float z)
    {
        for (int i = 0; i < xPositions.Length; i++)
        {
            float r = Random.Range(0f, 1f);

            if(r <= obstaclesRatio)
            {
                BuildObstacle(new Vector3(xPositions[i], y, z));
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
