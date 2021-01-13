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

    [SerializeField] private GameObject obstaclePrefab = null;

    private float[] xPositions;

    // Start is called before the first frame update
    void Start()
    {
        xPositions = new float[] { -4f, 0, 4f };

        BuildLevel();
    }

    private void BuildObstacle(Vector3 position)
    {
        GameObject obstacle = Instantiate(obstaclePrefab, position, Quaternion.identity);

        float r = Random.Range(0f, 1f);

        if(r > fansRatio)
        {
            obstacle.transform.GetChild(0).gameObject.SetActive(false);
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
        float y = startY;
        for (int i = 0; i < floorsSize; i++)
        {
            BuildFloor(y, z);
            y += offsetBetweenFloors;
        }
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
