using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private int layersSize = 0;
    [SerializeField] private float offsetBetweenLayers = 0f;
    [SerializeField] private float offsetBetweenFloors = 0f;
    [SerializeField] private float startX = 0f;
    [SerializeField] private float startY = 0f;
    [SerializeField] private float startZ = 0f;
    [SerializeField] private float deltaRandomX = 0f;
    [SerializeField] private float deltaRandomY = 0f;

    [SerializeField][Range(0f, 1f)] private float ringRatio = 0f;
    [SerializeField][Range(0f, 1f)] private float fanRatio = 0f;

    [SerializeField] private GameObject[] ringPrefabs = null;
    [SerializeField] private GameObject fanPrefab = null;

    private MovableObjectBuilder mob;
    private ScalableObjectBuilder sob;

    // Start is called before the first frame update
    void Start()
    {
        mob = GetComponent<MovableObjectBuilder>();
        sob = GetComponent<ScalableObjectBuilder>();

        BuildLevel();
    }

    private void BuildObject(GameObject objectToBuild, Vector3 position, bool movable, bool scalable)
    {
        GameObject go = Instantiate(objectToBuild, position, Quaternion.identity);

        if(movable)
        {
            mob.SetMovableObject(go.GetComponent<MovableObjectComponent>());
        }
        if(scalable)
        {
            sob.SetScalableObject(go.GetComponent<ScalableObjectComponent>());
        }
    }

    private void BuildFloor(int floorIndex, float y)
    {
        int floorGO;
        float z;
        switch (floorIndex)
        {
            case 0:
                floorGO = 0;
                z = startZ - 50f;
                break;
            case 2:
                floorGO = 2;
                z = startZ;
                break;
            default:
                floorGO = 1;
                z = startZ;
                break;
        }

        for (int i = 0; i < layersSize; i++)
        {
            BuildLayer(floorGO, y, z);
            z += offsetBetweenLayers;
        }
    }

    private void BuildLayer(int objectToSpawn, float y, float z)
    {
        float rx = Random.Range(startX - deltaRandomX, startX + deltaRandomX);
        float ry = Random.Range(y - deltaRandomY, y + deltaRandomY);

        if (objectToSpawn == 0)
        {
            BuildObject(fanPrefab, new Vector3(rx, ry, z), false, false);
        }
        else if(objectToSpawn == 2)
        {
            BuildObject(GetRandomRing(), new Vector3(rx, ry, z), true, true);
        }
        else
        {
            float r = Random.Range(0f, 1f);

            if (r <= ringRatio)
            {
                BuildObject(GetRandomRing(), new Vector3(rx, ry, z), true, true);
            }
            else if(r <= ringRatio + fanRatio)
            {
                BuildObject(fanPrefab, new Vector3(rx, ry, z), true, false);
            }
        }
    }

    private GameObject GetRandomRing()
    {
        if(ringPrefabs.Length > 0)
        {
            return ringPrefabs[Random.Range(0, ringPrefabs.Length)];
        }

        return null;
    }

    public void BuildLevel()
    {
        float y = startY;
        for (int i = 0; i < 3; i++)
        {
            BuildFloor(i, y);
            y += offsetBetweenFloors;
        }
    }

}
