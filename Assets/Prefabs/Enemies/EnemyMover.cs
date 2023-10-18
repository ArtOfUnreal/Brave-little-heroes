using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))] 

public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f,5f)] float movementSpeed = 1f;

    List<Node> path = new List<Node>();

    Enemy enemy;
    GridManager gridManager;
    Pathfinder pathfinder;

    void OnEnable()
    {
        JumpToStart();
        RecalculatePath(true);
        //StartCoroutine(FollowPath());
    }

    private void Awake()
    {
        enemy = this.GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();

        if (resetPath)
        {
            coordinates = pathfinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(this.transform.position);
        }
        StopAllCoroutines();
        path.Clear();
        path = pathfinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    void JumpToStart()
    {
        this.transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }

    IEnumerator FollowPath() 
    {
        for (int i = 1; i < path.Count; i++)
        {
            Vector3 startPosition = this.transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float traverPercent = 0f;
            this.transform.LookAt(endPosition);

            while (traverPercent <= 1f)
            {
                traverPercent += Time.deltaTime * movementSpeed;
                this.transform.position = Vector3.Lerp(startPosition, endPosition, traverPercent);
                yield return new WaitForEndOfFrame();
            }
            //yield return new WaitForSeconds(waitTime);
        }
        ReachedEndOfPath();
    }

    void ReachedEndOfPath() 
    { 
        enemy.GivePenalty();
        gameObject.SetActive(false);
    }
}
