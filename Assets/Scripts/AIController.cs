using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] private float obstacleDetectionDistance;
    [SerializeField] private float releaseThreshold;
    [SerializeField] private float diveThreshold;
    [SerializeField] private LayerMask lmObstaclesToAvoid;

    private enum State { Release, Dive, SeekBoosts }
    private enum MovementDirection { Up, Down, Left, Right, Forward }

    private HangGliderComponent hgc;
    private State currentState;

    void Awake()
    {
        hgc = GetComponent<HangGliderComponent>();

        currentState = State.Release;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= releaseThreshold)
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
        if(hgc.isPropelled)
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

    void SeekBoosts()
    {

    }

    void CheckObstacles()
    {
        Vector3 rayStartPosition = transform.position + new Vector3(0, 1, 0);

        bool forwardObstacle = Physics.Raycast(rayStartPosition, transform.rotation * new Vector3(0, 0, 1), obstacleDetectionDistance, lmObstaclesToAvoid);
        bool leftObstacle = Physics.Raycast(rayStartPosition, transform.rotation * new Vector3(1, 0, 0), obstacleDetectionDistance, lmObstaclesToAvoid);
        bool rightObstacle = Physics.Raycast(rayStartPosition, transform.rotation * new Vector3(-1, 0, 0), obstacleDetectionDistance, lmObstaclesToAvoid);

        Debug.DrawLine(rayStartPosition, rayStartPosition + (transform.rotation * new Vector3(0, 0, 1) * obstacleDetectionDistance));
        Debug.DrawLine(rayStartPosition, rayStartPosition + (transform.rotation * new Vector3(1, 0, 0) * obstacleDetectionDistance));
        Debug.DrawLine(rayStartPosition, rayStartPosition + (transform.rotation * new Vector3(-1, 0, 0) * obstacleDetectionDistance));

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
}
