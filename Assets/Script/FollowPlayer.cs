using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] float speed = 0.05f;
    private PlayerController controller;
    private Animator animator;
    private Rigidbody rb;
    //private Pathfinding pathFinding;


    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find ("Player").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        //pathFinding = GetComponent<Pathfinding>(); 
    }

    // Update is called once per frame
    void Update()
    {
        FollowAttack();
        ControlAnimation();
        //pathFinding.FindPath(transform.position, controller.transform.position);
        //StartCoroutine(FollowPath());
    }

    void FollowAttack()
    {
        Vector3 followDirection = transform.position - controller.transform.position;
        transform.Translate(followDirection * speed * Time.deltaTime);
        
    }

    void ControlAnimation()
    {
        if (rb.velocity.magnitude >0)
        {
            animator.SetTrigger("walk");
        }
    }

    //IEnumerator FollowPath()
    //{
    //    Vector3 currentWaypoint = pathFinding.path[0];

    //    while (true)
    //    {
    //        if (transform.position == currentWaypoint)
    //        {
    //            inDex ++;
    //            if (inDex >= pathFinding.path.Length)
    //            {
    //                yield break;
    //            }
    //            currentWaypoint = pathFinding.path[inDex];
    //        }

    //        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
    //        yield return null;

    //    }
    //}
}
