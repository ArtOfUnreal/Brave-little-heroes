using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))] 

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<TileHandler> path = new List<TileHandler>();
    [SerializeField] [Range(0f,5f)] float movementSpeed = 1f;
    Enemy enemy;

    void OnEnable()
    {
        FindPath();
        JumpToStart();
        StartCoroutine(FollowPath());
    }

    private void Start()
    {
        enemy = this.GetComponent<Enemy>();
    }

    void FindPath()
    {
        path.Clear();
        GameObject parent = GameObject.FindGameObjectWithTag("Path");
        foreach (Transform child in parent.transform)
        {
            TileHandler waypoint = child.GetComponent<TileHandler>();
            if (waypoint != null)
            {
                path.Add(waypoint);
            }
            
        }
    }

    void JumpToStart()
    {
        this.transform.position = path[0].transform.position;
    }

    IEnumerator FollowPath() 
    {
        foreach (TileHandler waypoint in path) 
        {
            Vector3 startPosition = this.transform.position;
            Vector3 endPosition = waypoint.transform.position;
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
