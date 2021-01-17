using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] private float obstacleDetectionDistance = 0f;
    [SerializeField] private float releaseThreshold = 0f;
    [SerializeField] private float diveThreshold = 0f;
    [SerializeField] private LayerMask lmObstaclesToAvoid;

    [SerializeField] private float sphereBoostsDetectionRadius = 0f;
    [SerializeField] private LayerMask lmBoostsToSeek;
    [SerializeField] private int targetBoostIndex = 0;
    [SerializeField][Range(0f, 1f)] private float maxSpeedMultiplier = 0;

    [SerializeField]private Transform playerTransform = null;

    private enum State { Release, Dive, SeekBoosts }
    private enum MovementDirection { Up, Down, Left, Right, Forward }

    private HangGliderComponent hgc;
    private State currentState;
    private float speedMultiplier;
    private LevelManager lm;

    void Awake()
    {
        hgc = GetComponent<HangGliderComponent>();
    }

    void Start()
    {
        lm = LevelManager.Instance;

        hgc.rotateX = false;

        currentState = State.Release;

        if (lm != null)
        {
            speedMultiplier = 1f + Mathf.Clamp(0.01f * lm.CurrentLevel, 0f, maxSpeedMultiplier);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(hgc.HasEnded)
        {
            return;
        }

        SetPlayParticles();

        if (transform.position.y <= releaseThreshold)
        {
            currentState = State.Release;
        }
        else if(transform.position.y >= diveThreshold)
        {
            currentState = State.Dive;
        }
        else
        {
            currentState = State.SeekBoosts;
        }

        if(hgc.isPropelled)
        {
            return;
        }

        CheckObstacles();
    }

    void FixedUpdate()
    {
        if (hgc.HasEnded)
        {
            hgc.PlayEndBehaviour();
            return;
        }

        if (hgc.isPropelled)
        {
            return;
        }

        switch (currentState)
        {
            case State.Release:
                hgc.Release();
                break;
            case State.Dive:
                hgc.Dive();
                break;
            case State.SeekBoosts:
                SeekBoosts();
                break;
            default:
                break;
        }
    }

    void SetPlayParticles()
    {
        if(playerTransform == null)
        {
            return;
        }

        float z = transform.position.z;
        float zPlayer = playerTransform.position.z;
        hgc.PlayParticles = z >= zPlayer && z - zPlayer <= 100f; 
    }

    void SeekBoosts()
    {
        Collider[] boosts = Physics.OverlapSphere(transform.position + new Vector3(0, 0, sphereBoostsDetectionRadius), sphereBoostsDetectionRadius, lmBoostsToSeek);

        if(boosts.Length > targetBoostIndex)
        {
            hgc.MoveTowards(boosts[targetBoostIndex].transform.position, speedMultiplier);
        }
        else
        {
            Move(MovementDirection.Forward);
            Move(MovementDirection.Down);
        }
    }

    void CheckObstacles()
    {
        Vector3 rayStartPosition = transform.position + new Vector3(0, 1, 0);

        bool forwardObstacle = Physics.Raycast(rayStartPosition, transform.rotation * new Vector3(0, 0, 1), obstacleDetectionDistance, lmObstaclesToAvoid);
        bool leftObstacle = Physics.Raycast(rayStartPosition, transform.rotation * new Vector3(-1, 0, 0), obstacleDetectionDistance, lmObstaclesToAvoid);
        bool rightObstacle = Physics.Raycast(rayStartPosition, transform.rotation * new Vector3(1, 0, 0), obstacleDetectionDistance, lmObstaclesToAvoid);

        Debug.DrawLine(rayStartPosition, rayStartPosition + (transform.rotation * new Vector3(0, 0, 1) * obstacleDetectionDistance));
        Debug.DrawLine(rayStartPosition, rayStartPosition + (transform.rotation * new Vector3(-1, 0, 0) * obstacleDetectionDistance));
        Debug.DrawLine(rayStartPosition, rayStartPosition + (transform.rotation * new Vector3(1, 0, 0) * obstacleDetectionDistance));

        if (leftObstacle)
        {
            Move(MovementDirection.Right);
        }
        if(rightObstacle)
        {
            Move(MovementDirection.Left);
        }
        if(forwardObstacle)
        {
            if (!FindForwardWay(rayStartPosition, transform.rotation * new Vector3(0, 0, 1)))
            {
                hgc.Release();
            }
        }

        if(!leftObstacle && !rightObstacle && !forwardObstacle)
        {
            Move(MovementDirection.Forward);
        }
    }

    bool FindForwardWay(Vector3 origin, Vector3 direction)
    {
        bool leftForward = Physics.Raycast(origin, Quaternion.Euler(0, -30, 0) * direction, obstacleDetectionDistance, lmObstaclesToAvoid);
        bool rightForward = Physics.Raycast(origin, Quaternion.Euler(0, 30, 0) * direction, obstacleDetectionDistance, lmObstaclesToAvoid);

        if(!leftForward)
        {
            Move(MovementDirection.Left);
        }
        if(!rightForward)
        {
            Move(MovementDirection.Right);
        }

        Debug.DrawLine(origin, origin + (Quaternion.Euler(0, 30, 0) * direction * obstacleDetectionDistance));
        Debug.DrawLine(origin, origin + (Quaternion.Euler(0, -30, 0) * direction * obstacleDetectionDistance));

        if(leftForward && rightForward)
        {
            return false;
        }

        return true;
    }

    void Move(MovementDirection movementDirection)
    {
        switch (movementDirection)
        {
            case MovementDirection.Up:
                hgc.Release();
                break;
            case MovementDirection.Down:
                hgc.Dive();
                break;
            case MovementDirection.Left:
                hgc.Turn(-0.2f);
                break;
            case MovementDirection.Right:
                hgc.Turn(0.2f);
                break;
            case MovementDirection.Forward:
                hgc.Turn(0);
                break;
            default:
                break;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, 0, sphereBoostsDetectionRadius), sphereBoostsDetectionRadius);
    }
}
