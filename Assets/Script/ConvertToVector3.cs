using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class ConvertToVector3 : MonoBehaviour
{
    private Pathfinding pathFinding; 
    [SerializeField] float speed = 2f;
    int targetIndex = 0;

    private void Awake()
    {
        
    }
    void Start()
    {
        pathFinding = GameObject.Find("A*").GetComponent<Pathfinding>();
        StartCoroutine(FollowPath());
    }

    private void Update()
    {
       
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = pathFinding.path[0];

        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= pathFinding.path.Length)
                {
                    yield break;
                }
                currentWaypoint = pathFinding.path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;

        }
    }
}
